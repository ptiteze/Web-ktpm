using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Kitchen_MVC.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{

			return View();
		}
	}
}
