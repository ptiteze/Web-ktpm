using AutoMapper;
using Kitchen_MVC.Commons.Enums;
using Kitchen_MVC.Commons.Exceptions;
using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.Account;
using Kitchen_MVC.DTO.Customer;
using Kitchen_MVC.DTO.Mail;
using Kitchen_MVC.DTO.CartDetail;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.Services;
using Kitchen_MVC.Singleton;

namespace Kitchen_MVC.Repositores
{
	public class CustomerRepository : ICustomerRepository
	{
		//private readonly DataContext SingletonDataBridge.GetInstance();
		//private readonly IMapper SingletonAutoMapper.GetInstance();
		private readonly IUploadService _upload;
		private readonly IMailService _mail;
		private readonly IOtpService _otp;
		private readonly IAccountRepository _accountRepository;
		public CustomerRepository(/*DataContext dataContext, IMapper mapper,*/ IUploadService upload,
			IMailService mail, IOtpService otp, IAccountRepository accountRepository)
		{
			//SingletonDataBridge.GetInstance() = dataContext;
			//SingletonAutoMapper.GetInstance() = mapper;
			_upload = upload;
			_mail = mail;
			_otp = otp;
			_accountRepository = accountRepository;
		}

		public async Task<bool> ActiveAccount(VerifyOTPRequest request)
		{
			//await _accountRepository.VerifyOTP(request);
			var account = SingletonDataBridge.GetInstance().Accounts.FirstOrDefault(x => x.Email == request.Email);
			account.Status = true;

			SingletonDataBridge.GetInstance().Accounts.Update(account);
			SingletonDataBridge.GetInstance().SaveChanges();

			return true;
		}

		public async Task<bool> CreateCustomer(CreateCustomerRequest request)
		{
			try
			{
				Customer customer = SingletonAutoMapper.GetInstance().Map<Customer>(request);
				// Quản trị viên, Khách hàng, Nhân viên
				var roleCustomer = await SingletonDataBridge.GetInstance().Roles.FindAsync(2);
				var account = new Account()
				{
					Email = request.Email,
					Password = request.Password,
					Role = roleCustomer,
					RoleId = roleCustomer.Id,
					Status = true
				};
				customer.EmailNavigation = account;
				account.Customers.Add(customer);
				SingletonDataBridge.GetInstance().Add(account);
				SingletonDataBridge.GetInstance().Add(customer);
				SingletonDataBridge.GetInstance().SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
			
		}

		public async Task<List<CartDetailDTO>> GetCartDetailsByCustomerId(int id)
		{
			var cartDetails = SingletonDataBridge.GetInstance().CartDetails.Where(c => c.CustomerId == id).ToList();	
			var cartDetailDTOs = new List<CartDetailDTO>();
			cartDetails.ForEach(x => cartDetailDTOs.Add(SingletonAutoMapper.GetInstance().Map<CartDetailDTO>(x)));
			return cartDetailDTOs;
		}
		public CustomerDTO GetCustomerById(int id)
		{
			var res = SingletonAutoMapper.GetInstance().Map<CustomerDTO>(SingletonDataBridge.GetInstance().Customers.Where(c => c.Id==id).FirstOrDefault());
			if (res == null)
				throw new NotFoundException("Not find customer with id: " + id);
			return res;
		}

		public async Task<bool> UpdateCustomer(int id, UpdateCustomerRequest request)
		{
			try
			{
				var customer = SingletonDataBridge.GetInstance().Customers.Find(id);
				if(customer == null) throw new NotFoundException();
				customer.Fullname = request.Fullname;
				customer.PhoneNumber = request.PhoneNumber;
				customer.Address = request.Address;
				SingletonDataBridge.GetInstance().Update(customer);
				SingletonDataBridge.GetInstance().SaveChangesAsync();
				return true;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}
	}
}
