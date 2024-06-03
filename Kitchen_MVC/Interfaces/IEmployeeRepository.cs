using Kitchen_MVC.DTO.Account;
using Kitchen_MVC.DTO.Employee;
using Kitchen_MVC.Models;

namespace Kitchen_MVC.Interfaces
{
    public interface IEmployeeRepository
    {

        ICollection<Employee> ListEmployee();

        Task<Employee> GetEmployeeById(int id);

        Task<bool> CreateEmployee(CreateEmployeeRequest request);

        Task<bool> UpdateEmployee(int productId ,UpdateEmployeeRequest request);

        Task<bool> DeleteEmployee(int id);

        Task<ICollection<Employee>> PagingEmployee(int? page, int? size);
        Task<List<EmployeeDTO>> GetAllEmployees();
    }
}
