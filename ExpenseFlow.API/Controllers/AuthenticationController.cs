using ExpenseFlow.Application;
using ExpenseFlow.Application.Users;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ExpenseFlow.API.Controllers;

public class AuthenticationController : CustomBaseController
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationRequest userForRegistrationModel)
    {
        var result = await _authService.RegisterAsync(userForRegistrationModel);
        return CreateActionResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] UserLoginRequest user)
    {
        var loginResult = await _authService.LoginAsync(user);

        if (loginResult.IsFail)
            return CreateActionResult(ServiceResult<TokenResponse>.Fail("Giriş yapılamadı.", HttpStatusCode.Unauthorized));

        return CreateActionResult(ServiceResult<TokenResponse>.Success(loginResult.Data));
    }



    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenRefreshRequest tokenModel)
    {
        var refreshedToken = await _authService.RefreshTokenAsync(tokenModel);
        return CreateActionResult(refreshedToken);
    }




}


