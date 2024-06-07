using Kitchen_MVC.DTO.Product;

namespace Kitchen_MVC.ViewModels.Admin
{
	public class EditProductViewModel
	{
		public ProductDTO Product { get; set; }

		public UpdateProductRequest request { get; set; }
	}
}
