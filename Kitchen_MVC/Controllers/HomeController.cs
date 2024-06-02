using AutoMapper;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kitchen_MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ICategoryRepository _clientCategory;
		private readonly IProductRepository _clientProduct;
		private readonly IMapper _mapper;
		public HomeController(ILogger<HomeController> logger,
			ICategoryRepository clientCategory, IProductRepository clientProduct, IMapper mapper)
		{
			_logger = logger;
			_clientCategory = clientCategory;
			_clientProduct = clientProduct;
			_mapper = mapper;
		}

		public HttpContext GetHttpContext()
		{
			return HttpContext;
		}

		public IActionResult Index()
		{
			List<CategoryDTO> categories = _clientCategory.GetAllCategories();
			List<ProductDTO> products = _clientProduct.GetAllProducts();
			List<ImageDTO> images = new List<ImageDTO>();
			foreach (ProductDTO product in products)
			{
				List<ImageDTO> imagesTemp = _clientProduct.GetImageById(product.Id);
				if (imagesTemp != null && imagesTemp.Count > 0)
					images.Add(imagesTemp[0]);
			}
			HomeViewModels Models = new HomeViewModels
			{
				Categories = categories,
				Products = products,
				Images = images
			};

			return View(Models);
		}


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
