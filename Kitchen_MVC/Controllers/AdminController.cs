using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.Models;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.ViewModels.Admin;
using Microsoft.IdentityModel.Tokens;
using Kitchen_MVC.ViewModels.Customer;
using Kitchen_MVC.ViewModels.Order;
using Kitchen_MVC.ViewModels.Image;
using Kitchen_MVC.DTO.Image;

namespace Kitchen_MVC.Controllers
{
	public class AdminController : Controller
	{
		private readonly IProductRepository _productRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IImageRepository _imageRepository;

		public AdminController(IProductRepository productRepository, ICategoryRepository categoryRepository,IImageRepository imageRepository,
			ICustomerRepository customerRepository, IOrderRepository orderRepository, IEmployeeRepository employeeRepository)
		{
			_customerRepository = customerRepository;
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_orderRepository = orderRepository;
			_employeeRepository = employeeRepository;
			_imageRepository = imageRepository;
		}

		public IActionResult Index()
		{

			return View();
		}

		public async Task<IActionResult> ManageProduct()
		{
			List<ProductDTO> products = await _productRepository.GetAllProducts();
			ManageProductViewModel Model = new ManageProductViewModel()
			{
				Products = products,
			};
			return View(Model);
		}
		[HttpGet]
		public IActionResult EditProduct(int id)
		{
			var productDto = _productRepository.GetProductById(id);
			var viewModel = new EditProductViewModel()
			{
				Product = productDto
			};
			return View(viewModel);
		}
		[HttpPost]
		public  async Task<IActionResult> EditProduct(int id, UpdateProductRequest request)
		{

			var res = await _productRepository.UpdateProduct(id, request);
			return RedirectToAction("ManageProduct", "Admin");
		}

		[HttpGet]
		public IActionResult DeleteProduct(int id)
		{
			var res = _productRepository.DeleteProduct(id);
			return RedirectToAction("ManageProduct", "Admin");
		}
		[HttpGet]
		public IActionResult AddProduct()
		{
			var categories = _categoryRepository.GetAllCategories().Result;
			var modelView = new AddProductViewModel()
			{
				Categories = categories
			};
			return View(modelView);
		}

		public IActionResult AddProduct(CreateProductRequest request)
		{
			if(!ModelState.IsValid)
			{
				return View(request);
			}
			//request.CategoryId = 3;
			var res = _productRepository.CreateProduct(request);
			return RedirectToAction("Index", "Admin");
		}
		//Category
		public IActionResult ManageCategory()
		{
			List<CategoryDTO> categories = _categoryRepository.GetAllCategories().Result;
			ManageCategoryViewModel Model = new ManageCategoryViewModel()
			{
				Categories = categories
			};
			return View(Model);
		}

		[HttpGet]
		public IActionResult EditCategory(int id)
		{
			var category = _categoryRepository.GetCategoryById(id);
			var categoryModel = new EditCategoryViewModel()
			{
				Category = category,
			};
			return View(categoryModel);
		}
		[HttpPost]
		public IActionResult EditCategory(int id, UpdateCategoryRequest request)
		{
			var res = _categoryRepository.UpdateCategory(id, request);
			return RedirectToAction("ManageCategory", "Admin");
		}

		public IActionResult DeleteCategory(int id)
		{
			var products = _categoryRepository.GetProductsByCategoryId(id);
			if (!products.IsNullOrEmpty())
			{
				return RedirectToAction("ManageCategory", "Admin");
			}
			_categoryRepository.DeleteCategory(id);
			return RedirectToAction("ManageCategory", "Admin");
		}
		[HttpGet]
		public IActionResult AddCategory()
		{
			return View();
		}

		[HttpPost]
		public IActionResult AddCategory(CreateCategoryRequest request)
		{
			_categoryRepository.CreateCategory(request);
			return RedirectToAction("ManageCategory", "Admin");
		}

		// customer

		public async Task<IActionResult> ManageCustomer()
		{
			var customerDtos = await _customerRepository.GetListCustomer();
			var customerViewModel = new ManageCustomerViewModel()
			{
				CustomerDTOs = customerDtos
			};
			return View(customerViewModel);
		}
		public IActionResult DeleteCustomer(int id)
		{
			var res = _customerRepository.DeleteCustomer(id);
			return RedirectToAction("ManageCustomer", "Admin");
		}

		//Order
		//Status 0: Chưa xác nhận
		//Status 1: Thành công
		//Status 2: Hủy
		public IActionResult ManageOrder()
		{
			var orderDtos = _orderRepository.GetAllOrders();
			var customerDtos = _customerRepository.GetListCustomer().Result;
			var employeeDtos = _employeeRepository.GetAllEmployees().Result;
			var viewModel = new ManageOrderViewModel()
			{
				Orders = orderDtos,
				Customer = customerDtos,
				Employees = employeeDtos
			};
			return View(viewModel);
		}

		public IActionResult ConfirmOrder(int id)
		{
			var res = _orderRepository.ConfirmOrder(id);
			return RedirectToAction("Index", "Admin");
		}

		//Image
		[HttpGet]
		public async Task<IActionResult> ManageImage()
		{

            List<ProductDTO> products = await _productRepository.GetAllProducts();
            AddImageViewModel Model = new AddImageViewModel()
            {
                Products = products,
            };
            return View(Model);
		}
		[HttpPost]
		public async Task<IActionResult> ManageImage(CreateImageRequest request)
		{
			var res = await _imageRepository.CreateImage(request);
            return RedirectToAction("Index", "Admin");
        }
	}
}

