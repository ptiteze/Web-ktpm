using AutoMapper;
using Kitchen_MVC.DTO.Account;
using Kitchen_MVC.DTO.CartDetail;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Customer;
using Kitchen_MVC.DTO.Employee;
using Kitchen_MVC.DTO.Image;
using Kitchen_MVC.DTO.Order;
using Kitchen_MVC.DTO.OrderDetail;
using Kitchen_MVC.DTO.Product;
using Kitchen_MVC.DTO.Role;
using Kitchen_MVC.Models;

namespace Kitchen_MVC.Helper
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {
            CreateMap<Role, RoleDTO>();

            //Employee
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<CreateEmployeeRequest, Employee>();
            CreateMap<UpdateEmployeeRequest, Employee>();

            //Category
            CreateMap<Category, CategoryDTO>();
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<UpdateCategoryRequest, Category>();

            //Account
            CreateMap<Account, AccountDTO>();

            //Product
            CreateMap<Product, ProductDTO>();
            //Image
            CreateMap<Image, ImageDTO>();
            //Customer
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CreateCustomerRequest, Customer>();
            //CartDetail
            CreateMap<CartDetail, CartDetailDTO>();
            CreateMap<CreateCartDetailRequest, CartDetail>();
            CreateMap<UpdateCartDetailRequest, CartDetail>();
            //Order
            CreateMap<Order, OrderDTO>();
            CreateMap<CreateOrderRequest, Order>();
            //OrderDetail
            CreateMap<Orderdetail, OrderDetailDTO>();
            CreateMap<CreateOrderDetailRequest, Orderdetail>();
		}
	}
}
