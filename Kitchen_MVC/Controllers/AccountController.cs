using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.Account;
using Kitchen_MVC.DTO.Customer;
using Kitchen_MVC.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kitchen_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;   
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountController(ILogger<AccountController> logger, IAccountRepository accountRepository
            ,IHttpContextAccessor contextAccessor, ICustomerRepository customerRepository)
        {
            _contextAccessor = contextAccessor;
            _logger = logger;
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }

        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAuthRequest request)
        {
            if(request.Email == null || request.Password == null)
            {
                ViewBag.error = "Invalid password, username Try again !!";
                return View("Login");
            }

            var infoAccount = await _accountRepository.Login(request);
            if(infoAccount == null)
            {
                ViewBag.error = "Login error, Try again !!!";
                return View("Login");
            }

			HttpContext.Session.SetString("Username", infoAccount.Email);
			HttpContext.Session.SetString("Fullname", infoAccount.Fullname);
			HttpContext.Session.SetString("RoleId", infoAccount.RoleId.ToString());

			if (infoAccount.RoleId == 1) // Quản trị employee
            {
				return RedirectToAction("Index", "Admin");
			}
            else
            {
				return RedirectToAction("Index", "Home");
			}
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
			HttpContext.Session.Clear();

			return RedirectToAction("Index", "Home");
		}

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateCustomerRequest request)
        {
            if(!ModelState.IsValid)
            {
				ViewBag.error = "Không được để trống dữ liệu.Try again !!!";
				return View();
			}
            var account = await _accountRepository.findAccount(request.Email);
            if(account != null)
            {
				ViewBag.error = "Đã có tài khoản sử dụng email này , vui lòng kiểm tra lại !!!";
				return View();
			}    
            var res = await _customerRepository.CreateCustomer(request);
            
            if(!res)
            {
				ViewBag.error = "Đã có tài khoản sử dụng email này , vui lòng kiểm tra lại !!!";
				return View();
			}    

            return View("Login");
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
