using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Customer;

namespace Kitchen_MVC.ViewModels.Account
{
	public class ChangeInfoViewModel
	{
		public CustomerDTO Customer { get; set; }
		public List<CategoryDTO> Categories { get; set; }
	}
}
