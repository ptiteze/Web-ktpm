using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.Repositores;
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
		public IActionResult Index(string input, string option)
		{
			List<CategoryDTO> categories = _clientCategory.GetAllCategories().Result;
			List<ProductDTO> products = new List<ProductDTO>();
			List<ProductDTO> productsAZ = new List<ProductDTO>();
			if (!option.Equals("All"))
			{
				int CateId = categories.FirstOrDefault(c => c.Name == option).Id;
				products = _clientCategory.GetProductsByCategoryId(CateId);
			}
			else
			{
				products = _clientProduct.GetAllProducts();
			}
			if (input == null || input.Trim().Equals(""))
			{
				productsAZ = products;
			}
			else
			{
				productsAZ = products.Where(p => p.Name.Contains(input.Trim())).ToList();
			}
			Dictionary<int, string> images = new Dictionary<int, string>(); // idProduct, ImageURL
			foreach (ProductDTO prd in productsAZ)
			{
				List<ImageDTO> imagesTemp = _clientProduct.GetImageById(prd.Id);
				if (imagesTemp != null && imagesTemp.Count > 0)
					images.Add(prd.Id,imagesTemp[0].Url);
			}
			var headerViewModel = new HeaderViewModel()
			{
				Categories = categories
			};
			ViewBag.HeaderData = headerViewModel;
			SearchProductViewModel Model = new SearchProductViewModel()
			{
				Categories = categories,
				Products = productsAZ,
				Images = images
			};
			return View(Model);
		}
		[HttpPost]
		public IActionResult GetSortedProducts([FromBody] SortedProductRequest request)
		{
			List<CategoryDTO> categories = _clientCategory.GetAllCategories().Result;
			var pros = request.products;
			Console.WriteLine(pros.Count);
			var listpr = GetProductsSorted(request);
			var model = new SearchProductViewModel
			{
				Categories = categories,
				Products = listpr,
				Images = GetProductImages(listpr)
			};

			// Trả về partial view với danh sách sản phẩm đã sắp xếp
			return PartialView("_ProductList", model);
		}
		private List<ProductDTO> GetProductsSorted(SortedProductRequest request)
		{
			var products = request.products; 
			switch (request.sortOption)
			{
				case "az":
					return products;
				case "za":
					products.Reverse();
					return products;
				case "stock":
					return products.OrderByDescending(p => p.Quantity).ToList();
				case "price":
					return products.OrderBy(p => p.Price).ToList();
				default:
					return products;
			}
		}
		private Dictionary<int, string> GetProductImages(List<ProductDTO> products) {
			Dictionary<int, string> images = new Dictionary<int, string>(); // idProduct, ImageURL
			foreach (ProductDTO prd in products)
			{
				List<ImageDTO> imagesTemp = _clientProduct.GetImageById(prd.Id);
				if (imagesTemp != null && imagesTemp.Count > 0)
					images.Add(prd.Id, imagesTemp[0].Url);
			}
			return images;
		}
		public IActionResult ProductDetail(int id)
		{
			List<CategoryDTO> categories = _clientCategory.GetAllCategories().Result;
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
