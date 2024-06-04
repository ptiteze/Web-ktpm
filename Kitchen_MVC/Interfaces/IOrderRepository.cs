using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Order;
using Kitchen_MVC.DTO.OrderDetail;

namespace Kitchen_MVC.Interfaces
{
    public interface IOrderRepository
    {
		Task<int> CreateOrder(CreateOrderRequest request);
		List<OrderDTO> GetAllOrders();
		OrderDTO GetOrderById(int id);
		List<OrderDetailDTO> GetOrderDetailsByOrderId(int id);
		List<OrderDTO> GetOrdersByCustomerId(int id);

	}
}
