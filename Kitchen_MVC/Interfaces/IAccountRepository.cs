using Kitchen_MVC.DTO.Account;

namespace Kitchen_MVC.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<AccountDTO>> listAccount();

        Task<AccountDTO> findAccount(string email);

        Task<AuthDTO> Authenticate(LoginAuthRequest request);

        Task<AuthDTO> RefreshToken(RefreshTokenRequest request);

        Task<bool> ResendOTP(ResendOTPRequest request);

        Task<bool> VerifyOTP(VerifyOTPRequest request);

        Task<bool> ForgotPassword(ForgotPasswordRequest request);

        Task<bool> ResetPassword(ResetPasswordRequest request);

        Task<bool> ChangePassword(ChangePasswordRequest request);
    }
}
