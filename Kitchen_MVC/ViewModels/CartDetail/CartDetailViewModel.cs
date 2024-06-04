using Kitchen_MVC.DTO.CartDetail;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Customer;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.Models;

namespace Kitchen_MVC.ViewModels.CartDetail
{
	public class CartDetailViewModel
	{
		public List<CategoryDTO> Categories { get; set; }
		public CustomerDTO Customer { get; set; }	
		public List<CartDetailDTO> CartDetails { get; set; }// PId, Quantity
		public List<ProductDTO> Products { get; set;}
		public Dictionary<int, string> Images { get; set; } // idProduct, ImageURL
	}
}
