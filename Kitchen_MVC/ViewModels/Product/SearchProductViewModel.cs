using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Product;

namespace Kitchen_MVC.ViewModels.Product
{
	public class SearchProductViewModel
	{
		public List<CategoryDTO> Categories { get; set; }
		public List<ProductDTO> Products { get; set; }
		public Dictionary<int, string> Images { get; set; }
	}
}
