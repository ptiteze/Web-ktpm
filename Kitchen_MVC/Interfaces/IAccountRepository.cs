using Kitchen_MVC.DTO.Account;

namespace Kitchen_MVC.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<AccountDTO>> listAccount();

        Task<AccountDTO> findAccount(string email);

        Task<InfoAccountDTO> Login(LoginAuthRequest request);


        Task<bool> ChangePassword(ChangePasswordRequest request);
    }
}
