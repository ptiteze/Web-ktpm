using Kitchen_MVC.DTO.Account;
using Kitchen_MVC.DTO.CartDetail;
using Kitchen_MVC.DTO.Customer;
using Kitchen_MVC.Models;
namespace Kitchen_MVC.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> ActiveAccount(VerifyOTPRequest request);
		Task<Customer> GetCustomerById(int id);
        Task<List<CartDetailDTO>> GetCartDetailsByCustomerId(int id);
		Task<bool> CreateCustomer(CreateCustomerRequest request);
	}
}
