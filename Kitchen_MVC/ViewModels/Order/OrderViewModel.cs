using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Customer;
using Kitchen_MVC.DTO.Employee;
using Kitchen_MVC.DTO.Order;

namespace Kitchen_MVC.ViewModels.Order
{
	public class OrderViewModel
	{
		public List<CategoryDTO> Categories { get; set; }
		public CustomerDTO Customer { get; set; }	
		public List<OrderDTO> Orders { get; set; }
		public List<EmployeeDTO> Employees { get; set; }
	}
}
