using AutoMapper;
using Kitchen_MVC.Commons.Enums;
using Kitchen_MVC.Commons.Exceptions;
using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.Account;
using Kitchen_MVC.DTO.Employee;
using Kitchen_MVC.DTO.Mail;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.Services;
using Kitchen_MVC.Singleton;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace Kitchen_MVC.Repositores
{
	public class EmployeeRepository : IEmployeeRepository
    {
        //private readonly DataContext SingletonDataBridge.GetInstance();
        //private readonly IMapper SingletonAutoMapper.GetInstance();
        private readonly IUploadService _upload;
        private readonly IMailService _mail;
        private readonly IOtpService _otp;
        private readonly IAccountRepository _accountRepository;

        public EmployeeRepository(/*DataContext dataContext, IMapper mapper,*/ IUploadService upload
            , IMailService mail, IOtpService otp, IAccountRepository accountRepository)
        {
            //SingletonDataBridge.GetInstance() = dataContext;
            //SingletonAutoMapper.GetInstance() = mapper;
            _upload = upload;
            _mail = mail;
            _otp = otp;
            _accountRepository = accountRepository;
        }

        public async Task<bool> CreateEmployee(CreateEmployeeRequest request)
        {
            Employee employee = SingletonAutoMapper.GetInstance().Map<Employee>(request);
            // Quản trị viên, Khách hàng, Nhân viên
            var roleEmployee = await SingletonDataBridge.GetInstance().Roles.FindAsync(3);
            var account = new Account()
            {
                Email = request.Email,
                Password = request.Password,
                Role = roleEmployee,
                RoleId = roleEmployee.Id,
                Status = false
            };
            // image dưới database có lỗi cần đưa về nvarchar(100) để có thể lưu ảnh
            if(request.Image != null)
            {
                employee.Image = await _upload.UploadFile(request.Image);
            }

            employee.EmailNavigation = account;
            account.Employees.Add(employee);

            SingletonDataBridge.GetInstance().Add(account);
            SingletonDataBridge.GetInstance().Add(employee);
            SingletonDataBridge.GetInstance().SaveChanges();
            //generate otp
            var otp = _otp.GenerateOTP();

            ////save register otp 
            //var userToken = new AppUserToken()
            //{
            //    Token = otp,
            //    Type = TOKEN_TYPE.REGISTER_OTP,
            //    ExpiredAt = DateTime.Now.AddMinutes(TOKEN_TYPE.OTP_EXPIRY_MINUTES)
            //};

            //SingletonDataBridge.GetInstance().AppUserTokens.Add(userToken);
            //await SingletonDataBridge.GetInstance().SaveChangesAsync();

            //Send mail confirm
            var title = "Xác nhận đăng ký tài khoản";
            var name = employee.Fullname;
            _mail.sendMail(new CreateMailRequest()
            {
                Email = account.Email,
                Name = name,
                OTP = otp,
                Title = title,
                Type = MAIL_TYPE.REGISTATION
            });
            return true;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employee = await SingletonDataBridge.GetInstance().Employees.FindAsync(id)
                ?? throw new NotFoundException("Khong tim thay employee by " + id);

            var account = SingletonDataBridge.GetInstance().Accounts.FirstOrDefault(x => x.Email == employee.Email);
            account.Status = false;
            SingletonDataBridge.GetInstance().Accounts.Update(account);
            SingletonDataBridge.GetInstance().SaveChanges();
            return true;
        }

		public async Task<List<EmployeeDTO>> GetAllEmployees()
		{
            return SingletonAutoMapper.GetInstance().Map<List<EmployeeDTO>>(SingletonDataBridge.GetInstance().Employees.OrderBy(e => e.Id).ToList());    
		}

		public async Task<Employee> GetEmployeeById(int id)
        {
            var res = await  SingletonDataBridge.GetInstance().Employees.FindAsync(id);
            if (res == null)
                throw new NotFoundException("Not find employee with id: " + id);
            return res;
        }

        public ICollection<Employee> ListEmployee()
        {
            return SingletonDataBridge.GetInstance().Employees.ToList();
        }

        public Task<ICollection<Employee>> PagingEmployee(int? page, int? size)
        {
            int pageIndex = page ?? 1;
            int pageSize = size ?? 5;

            throw new NotImplementedException();
        }

        public async Task<bool> UpdateEmployee(int productId, UpdateEmployeeRequest request)
        {
            var employee = await SingletonDataBridge.GetInstance().Employees.FindAsync(productId)
                ?? throw new NotFoundException("Khong tim thay san pham by " + productId);

            employee = SingletonAutoMapper.GetInstance().Map(request, employee);

            if(request.Image != null)
            {
                employee.Image = await _upload.UploadFile(request.Image);
            }
            SingletonDataBridge.GetInstance().Employees.Update(employee);
            SingletonDataBridge.GetInstance().SaveChanges();
            return true;
        }
    }
}
