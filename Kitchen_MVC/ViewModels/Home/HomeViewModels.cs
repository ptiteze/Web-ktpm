using Kitchen_MVC.DTO;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Product;

namespace Kitchen_MVC.ViewModels.Home
{
	public class HomeViewModels
	{
		public List<CategoryDTO> Categories { get; set; }
		public List<ProductDTO> Products { get; set; }	
		public List<ImageDTO> Images { get; set; }
		public CategoryDTO Category { get; set; } = null;
	}
}
