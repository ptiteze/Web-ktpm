using Kitchen_MVC.DTO.Product;

namespace Kitchen_MVC.ViewModels.Product
{
	public class SortedProductRequest
	{
		public string sortOption { get; set; }
		public List<ProductDTO> products { get; set; }

	}
}
