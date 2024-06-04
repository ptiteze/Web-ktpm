using Kitchen_MVC.DTO.CartDetail;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Customer;
using Kitchen_MVC.DTO.Employee;
using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Order;
using Kitchen_MVC.DTO.OrderDetail;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.ViewModels.Header;
using Kitchen_MVC.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Kitchen_MVC.Controllers
{
	public class OrderController : Controller
	{
		private readonly ICartDetailRepository _cartDetailRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderdetailRepository _orderdetailRepository;
		private readonly IProductRepository _productRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IEmployeeRepository _employeeRepository;
		public OrderController(ICartDetailRepository cartDetailRepository, IOrderRepository orderRepository, IProductRepository productRepository, 
			ICustomerRepository customerRepository, IOrderdetailRepository orderdetailRepository, ICategoryRepository categoryRepository
			, IEmployeeRepository employeeRepository)
		{
			_cartDetailRepository = cartDetailRepository;
			_orderRepository = orderRepository;
			_productRepository = productRepository;
			_customerRepository = customerRepository;
			_orderdetailRepository = orderdetailRepository;
			_categoryRepository = categoryRepository;
			_employeeRepository = employeeRepository;
		}
		public IActionResult CompleteOrder(int id)
		{
			List<CartDetailDTO> cartDetails = _customerRepository.GetCartDetailsByCustomerId(id).Result;
			List<ProductDTO> products = _productRepository.GetAllProducts();
			CreateOrderRequest request = new CreateOrderRequest()
			{
				CustomerId = id,
				CreateAt = DateTime.Now,
				EmployeeId = null,
				PaymentStatus = false,
				Status = 1
			};
			int idNewOrder = 0;
			idNewOrder = _orderRepository.CreateOrder(request).Result;
			if(idNewOrder != 0) {
				foreach(var item in cartDetails)
				{
					CreateOrderDetailRequest odRequest = new CreateOrderDetailRequest()
					{
						OrderId = idNewOrder,
						Price = products.Where(p => p.Id == item.ProductId).FirstOrDefault().Price,
						ProductId = item.ProductId,
						Quantity = item.Quantity
					};
					_orderdetailRepository.CreateOrderDetail(odRequest);
					_cartDetailRepository.DeleteCartDetail(item.ProductId, id);
				}
				HttpContext.Session.SetString("Cartcount", "0");
			}
			return RedirectToAction("Index", "Cart", new { id = id.ToString() });
		}
		public IActionResult Index(string id)
		{
			int IdCustomer = int.Parse(id);
			List<CategoryDTO> categories = _categoryRepository.GetAllCategories().Result;
			CustomerDTO customer = _customerRepository.GetCustomerById(IdCustomer);
			List<OrderDTO> orders = _orderRepository.GetOrdersByCustomerId(IdCustomer);
			List<EmployeeDTO> employees = _employeeRepository.GetAllEmployees().Result;
			var headerViewModel = new HeaderViewModel()
			{
				Categories = categories
			};
			ViewBag.HeaderData = headerViewModel;
			OrderViewModel Model = new OrderViewModel()
			{
				Categories = categories,
				Customer = customer,
				Orders = orders,
				Employees = employees
			};
			return View(Model);
		}
		public IActionResult OrderDetail(int id)
		{
			OrderDTO order = _orderRepository.GetOrderById(id);
			CustomerDTO customer = _customerRepository.GetCustomerById(order.CustomerId);
			List<CategoryDTO> categories = _categoryRepository.GetAllCategories().Result;
			List<OrderDetailDTO> orderDetails = _orderRepository.GetOrderDetailsByOrderId(id);
			List<ProductDTO> products = new List<ProductDTO>();
			Dictionary<int, string> images = new Dictionary<int, string>(); // idProduct, ImageURL
			foreach (OrderDetailDTO orderDetail in orderDetails)
			{
				ProductDTO pr = _productRepository.GetProductById(orderDetail.ProductId);
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
			OrderDetailViewModel Model = new OrderDetailViewModel() { 
				Categories = categories,
				Customer = customer,
				Order = order,
				OrderDetails = orderDetails,
				Products = products,
				Images = images
			};
			return View(Model);
		}
	}
}
