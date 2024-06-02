using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.ViewModels.Header;
using Kitchen_MVC.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace Kitchen_MVC.Controllers
{
	public class ProductController : Controller
	{
		private readonly ICategoryRepository _clientCategory;
		private readonly IProductRepository _clientProduct;
		public ProductController(ICategoryRepository clientCategory, IProductRepository clientProduct)
		{
			_clientCategory = clientCategory;
			_clientProduct = clientProduct;
		}
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult ProductDetail(int id)
		{
			List<CategoryDTO> categories = _clientCategory.GetAllCategories();
			ProductDTO product = _clientProduct.GetProductById(id);
			List<ProductDTO> products = _clientCategory.GetProductsByCategoryId(product.CategoryId);
			List<ImageDTO> imageforProduct = _clientProduct.GetImageById(id);
			List<ImageDTO> images = new List<ImageDTO>();
			foreach (ProductDTO prd in products)
			{
				List<ImageDTO> imagesTemp = _clientProduct.GetImageById(prd.Id);
				if (imagesTemp != null && imagesTemp.Count > 0)
					images.Add(imagesTemp[0]);
			}
			var headerViewModel = new HeaderViewModel()
			{
				Categories = categories
			};
			ViewBag.HeaderData = headerViewModel;
			ProductViewModel Model = new ProductViewModel()
			{
				Categories = categories,
				ImagesForProduct = imageforProduct,
				Product = product,
				Products = products,
				Images = images
			};
			return View(Model);
		}
	}
}
