namespace ExpenseFlow.Application.Users;

public interface IAuthenticationService
{
    Task<ServiceResult> RegisterAsync(UserRegistrationRequest request);
    Task<ServiceResult<TokenResponse>> LoginAsync(UserLoginRequest request);
    Task<ServiceResult<TokenResponse>> RefreshTokenAsync(TokenRefreshRequest request);
    Task<ServiceResult<TokenResponse>> CreateTokenAsync(bool populateExp);

}

