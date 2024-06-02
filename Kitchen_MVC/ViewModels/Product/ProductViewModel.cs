using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Product;

namespace Kitchen_MVC.ViewModels.Product
{
	public class ProductViewModel
	{
		public List<CategoryDTO> Categories { get; set; }
		public List<ImageDTO> ImagesForProduct { get; set; }
		public List<ProductDTO> Products { get; set; }
		public List<ImageDTO> Images { get; set; }
		public ProductDTO Product { get; set; }
	}
}
