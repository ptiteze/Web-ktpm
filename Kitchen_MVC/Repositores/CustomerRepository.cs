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

namespace Kitchen_MVC.Repositores
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		private readonly IUploadService _upload;
		private readonly IMailService _mail;
		private readonly IOtpService _otp;
		private readonly IAccountRepository _accountRepository;
		public CustomerRepository(DataContext dataContext, IMapper mapper, IUploadService upload,
			IMailService mail, IOtpService otp, IAccountRepository accountRepository)
		{
			_dataContext = dataContext;
			_mapper = mapper;
			_upload = upload;
			_mail = mail;
			_otp = otp;
			_accountRepository = accountRepository;
		}

		public async Task<bool> ActiveAccount(VerifyOTPRequest request)
		{
			//await _accountRepository.VerifyOTP(request);
			var account = _dataContext.Accounts.FirstOrDefault(x => x.Email == request.Email);
			account.Status = true;

			_dataContext.Accounts.Update(account);
			_dataContext.SaveChanges();

			return true;
		}

		public async Task<bool> CreateCustomer(CreateCustomerRequest request)
		{
			try
			{
				Customer customer = _mapper.Map<Customer>(request);
				// Quản trị viên, Khách hàng, Nhân viên
				var roleCustomer = await _dataContext.Roles.FindAsync(2);
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
				_dataContext.Add(account);
				_dataContext.Add(customer);
				_dataContext.SaveChanges();

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
			
		}

		public async Task<List<CartDetailDTO>> GetCartDetailsByCustomerId(int id)
		{
			var cartDetails = _dataContext.CartDetails.Where(c => c.CustomerId == id).ToList();	
			var cartDetailDTOs = new List<CartDetailDTO>();
			cartDetails.ForEach(x => cartDetailDTOs.Add(_mapper.Map<CartDetailDTO>(x)));
			return cartDetailDTOs;
		}
		public async Task<Customer> GetCustomerById(int id)
		{
			var res = await _dataContext.Customers.FindAsync(id);
			if (res == null)
				throw new NotFoundException("Not find customer with id: " + id);
			return res;
		}
	}
}
