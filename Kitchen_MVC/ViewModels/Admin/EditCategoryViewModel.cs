using Kitchen_MVC.DTO.Category;

namespace Kitchen_MVC.ViewModels.Admin
{
	public class EditCategoryViewModel
	{
		public CategoryDTO Category { get; set; }

		public UpdateCategoryRequest request { get; set; }
	}
}
