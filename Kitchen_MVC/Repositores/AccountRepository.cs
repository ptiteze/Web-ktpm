using AutoMapper;
using Kitchen_MVC.Commons.Enums;
using Kitchen_MVC.Commons.Exceptions;
using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.Account;
using Kitchen_MVC.DTO.Mail;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.Singleton;

//using Kitchen_MVC.Services;
using Microsoft.AspNetCore.Authorization;
//using Newtonsoft.Json.Linq;

namespace Kitchen_MVC.Repositores
{
	public class AccountRepository : IAccountRepository
	{

		//private readonly DataContext _context;
		//private readonly IMapper _mapper;

		public AccountRepository(/*DataContext dataContext, IMapper mapper*/)
		{
			//_context = dataContext;
			//_mapper = mapper;
		}

		public async Task<InfoAccountDTO> Login(LoginAuthRequest request)
		{
			var account = SingletonDataBridge.GetInstance().Accounts.FirstOrDefault(x => x.Email == request.Email);
			if(account == null) 
			{
				return null;
            }

			if (!account.Password.Equals(request.Password))
			{
				//throw new NotFoundException("Password not match, try again!!!");
				return null;
			}

			if (!account.Status)
			{
				//throw new InvalidRequestException("Your account has been lockout");
				return null;
			}

			var infoAccount = new InfoAccountDTO();

			if(account.RoleId == 1)
			{
				var employee = SingletonDataBridge.GetInstance().Employees.FirstOrDefault(x => x.Email == request.Email);
				if(employee == null)
				{
					return null;
				}
				infoAccount.Fullname = employee.Fullname;
				infoAccount.Email = employee.Email;
				infoAccount.Id = employee.Id;
				infoAccount.RoleId = 1; 
			}	
			else if(account.RoleId == 2)
			{
				var customer = SingletonDataBridge.GetInstance().Customers.FirstOrDefault(x => x.Email == request.Email);
				if(customer == null)
				{
					return null;
				}	
				infoAccount.Fullname = customer.Fullname;
				infoAccount.Email = customer.Email;
				infoAccount.Id = customer.Id;
				infoAccount.RoleId = 2;
			}

			return infoAccount;
		}

		public async Task<bool> ChangePassword(ChangePasswordRequest request)
		{
			var account = SingletonDataBridge.GetInstance().Accounts.FirstOrDefault(x => x.Email == request.Email)
				?? throw new NotFoundException("Not find account by email, try again!!!");

			if(request.NewPassword != request.ConfirmPassword)
			{

			}

			account.Password = request.NewPassword;

			SingletonDataBridge.GetInstance().Accounts.Update(account);
			await SingletonDataBridge.GetInstance().SaveChangesAsync();
			return true;
		}

		public async Task<AccountDTO> findAccount(string email)
		{
			var account = SingletonDataBridge.GetInstance().Accounts.FirstOrDefault(x => x.Email == email);
			if (account == null)
			{
				//throw new NotFoundException("Can't find account by email");
				return null;
			}
			return SingletonAutoMapper.GetInstance().Map<AccountDTO>(account);
		}

		public Task<List<AccountDTO>> listAccount()
		{
			throw new NotImplementedException();
		}
	}
}
