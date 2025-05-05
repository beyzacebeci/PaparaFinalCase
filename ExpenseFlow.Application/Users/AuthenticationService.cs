using AutoMapper;
using ExpenseFlow.DataAccess.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseFlow.Application.Users;

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;


    private readonly UserManager<User> _userManager;
    private User? _user;


    public AuthenticationService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration, ILoggerService logger, RoleManager<IdentityRole> roleManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
        _roleManager = roleManager;
    }

    public async Task<ServiceResult<TokenResponse>> CreateTokenAsync(bool populateExp)
    {
        var signinCredentails = GetSigninCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signinCredentails, claims);
        var refreshToken = GenerateRefreshToken();

        _user.RefreshToken = refreshToken;

        if (populateExp)
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

        await _userManager.UpdateAsync(_user);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        var rolesList = (await _userManager.GetRolesAsync(_user)).ToList();

        var tokenResponse = new TokenResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            FirstName = _user.FirstName,
            LastName = _user.LastName,
            UserId = _user.Id,
            UserName = _user.UserName,
            Roles = rolesList
        };

        return ServiceResult<TokenResponse>.Success(tokenResponse);
    }

    public async Task<ServiceResult> RegisterAsync(UserRegistrationRequest request)
    {
        var user = _mapper.Map<User>(request);
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return ServiceResult.Fail(errors); // Hataları dön
        }

        foreach (var role in request.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                return ServiceResult.Fail($"Role '{role}' does not exist."); // Rol bulunamadı hatası
            }
        }

        var roleResult = await _userManager.AddToRolesAsync(user, request.Roles);
        if (!roleResult.Succeeded)
        {
            var errors = roleResult.Errors.Select(e => e.Description).ToList();
            return ServiceResult.Fail(errors); 
        }
        return ServiceResult.Success(); 
    }

    public async Task<ServiceResult<TokenResponse>> LoginAsync(UserLoginRequest request)
    {
        _user = await _userManager.FindByNameAsync(request.UserName);

        if (_user == null || !await _userManager.CheckPasswordAsync(_user, request.Password))
        {
            _logger.LogWarning($"{nameof(LoginAsync)} : Authentication failed. Incorrect username or password.");
            return ServiceResult<TokenResponse>.Fail("Incorrect username or password.");
        }

        return await CreateTokenAsync(populateExp: true); // DOĞRU ŞEKİLDE DÖN
    }
    private SigningCredentials GetSigninCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:secretKey"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, _user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, _user.Id)
        };

        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentails, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        return new JwtSecurityToken(
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signinCredentails);
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false, // Önemli: Süresi geçmiş token'ı kabul etmek için false
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["validIssuer"],
            ValidAudience = jwtSettings["validAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secretKey"]))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null ||
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token.");
        }

        return principal;
    }

    public async Task<ServiceResult<TokenResponse>> RefreshTokenAsync(TokenRefreshRequest request)
    {
        try
        {
            var principal = GetPrincipalFromExpiredToken(request.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity?.Name);

            if (user == null ||
                user.RefreshToken != request.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return ServiceResult<TokenResponse>.Fail("The refresh token is invalid or expired.");
            }

            _user = user;
            return await CreateTokenAsync(populateExp: false); // Direkt sonucu döndür
        }
        catch (Exception ex)
        {
            _logger.LogError($"RefreshTokenAsync error: {ex.Message}");
            return ServiceResult<TokenResponse>.Fail("An error occurred during the token refresh process.");

        }
    }


}

