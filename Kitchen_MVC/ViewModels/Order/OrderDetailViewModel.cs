using Kitchen_MVC.DTO.CartDetail;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Customer;
using Kitchen_MVC.DTO.Order;
using Kitchen_MVC.DTO.OrderDetail;
using Kitchen_MVC.DTO.Product;

namespace Kitchen_MVC.ViewModels.Order
{
	public class OrderDetailViewModel
	{
		public List<CategoryDTO> Categories { get; set; }
		public CustomerDTO Customer { get; set; }
		public List<OrderDetailDTO> OrderDetails { get; set; }// PId, Quantity
		public List<ProductDTO> Products { get; set; }
		public Dictionary<int, string> Images { get; set; } // idProduct, ImageURL
		public OrderDTO Order {  get; set; }
	}
}
