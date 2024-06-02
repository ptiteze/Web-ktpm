using Kitchen_MVC.DTO.CartDetail;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Repositores;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_MVC.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartDetailRepository _cartDetailRepository;
		public CartController(ICartDetailRepository cartDetailRepository)
		{
			_cartDetailRepository = cartDetailRepository;
		}
		[HttpPost]
		public IActionResult AddToCart(int ProductId, int CustomerId, int Quantity)
		{
			int Count = 0;
			if(_cartDetailRepository.CartExists(ProductId, CustomerId))
			{
				_cartDetailRepository.UpdateCartDetail(ProductId, CustomerId, Quantity);
			}else
			{
				CreateCartDetailRequest request = new CreateCartDetailRequest()
				{
					CustomerId = CustomerId,
					Quantity = Quantity,
					ProductId = ProductId
				};
				_cartDetailRepository.CreateCartDetail(request);
			}
			return Json(new
			{
				Count = _cartDetailRepository.GetCartCount(CustomerId)
			});
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
