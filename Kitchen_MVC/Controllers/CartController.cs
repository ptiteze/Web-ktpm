using Kitchen_MVC.DTO.CartDetail;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Customer;
using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Repositores;
using Kitchen_MVC.ViewModels.CartDetail;
using Kitchen_MVC.ViewModels.Header;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_MVC.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartDetailRepository _cartDetailRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IProductRepository _productRepository;
		private readonly ICategoryRepository _categoryRepository;
		
		public CartController(ICartDetailRepository cartDetailRepository, ICustomerRepository customerRepository, IOrderRepository orderRepository, IProductRepository productRepository, ICategoryRepository categoryRepository)
		{
			_cartDetailRepository = cartDetailRepository;
			_customerRepository = customerRepository;
			_orderRepository = orderRepository;
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
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
			Count = _cartDetailRepository.GetCartCount(CustomerId);
			HttpContext.Session.SetString("Cartcount", Count.ToString());
			return Json(new
			{
				Count
			});
		}
		public IActionResult Index(string id)
		{
			int Idcustomer = int.Parse(id);
			List<CategoryDTO> categories = _categoryRepository.GetAllCategories().Result;
			CustomerDTO customer = _customerRepository.GetCustomerById(Idcustomer);
			List<CartDetailDTO> cartDetails = _customerRepository.GetCartDetailsByCustomerId(Idcustomer).Result;
			List<ProductDTO> products = new List<ProductDTO>();
			Dictionary<int, string> images = new Dictionary<int, string>(); // idProduct, ImageURL
			foreach (CartDetailDTO cartDetail in cartDetails)
			{
				ProductDTO pr = _productRepository.GetProductById(cartDetail.ProductId);
				products.Add(pr);
			}
			foreach (ProductDTO prd in products)
			{
				List<ImageDTO> imagesTemp = _productRepository.GetImageById(prd.Id);
				if (imagesTemp != null && imagesTemp.Count > 0)
					images.Add(prd.Id, imagesTemp[0].Url);
			}
			var headerViewModel = new HeaderViewModel()
			{
				Categories = categories
			};
			ViewBag.HeaderData = headerViewModel;
			CartDetailViewModel Model = new CartDetailViewModel()
			{
				Categories = categories,
				Customer = customer,
				CartDetails = cartDetails,
				Products = products,
				Images = images
			};
			return View(Model);
		}
	}
}
