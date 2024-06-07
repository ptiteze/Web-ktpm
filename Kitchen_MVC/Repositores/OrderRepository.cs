using AutoMapper;
using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Order;
using Kitchen_MVC.DTO.OrderDetail;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.Singleton;

namespace Kitchen_MVC.Repositores
{
	public class OrderRepository : IOrderRepository
	{
		//private readonly DataContext SingletonDataBridge.GetInstance();
		//private readonly IMapper SingletonAutoMapper.GetInstance();
		public OrderRepository(/*DataContext dataContext, IMapper mapper*/)
		{
			//SingletonDataBridge.GetInstance() = dataContext;
			//SingletonAutoMapper.GetInstance() = mapper;
		}

        public async Task<bool> ConfirmOrder(int id)
        {
			var order = SingletonDataBridge.GetInstance().Orders.Find(id);
			order.Status = 1;
			await SingletonDataBridge.GetInstance().SaveChangesAsync();
			return true;
        }

        public async Task<int> CreateOrder(CreateOrderRequest request)
		{
			try
			{
				var order = SingletonAutoMapper.GetInstance().Map<Order>(request);
				SingletonDataBridge.GetInstance().Add(order);
				SingletonDataBridge.GetInstance().SaveChanges();
				return order.Id;
			}catch(Exception ex)
			{
				return 0;
			}
		}

		public List<OrderDTO> GetAllOrders()
		{
			return SingletonAutoMapper.GetInstance().Map<List<OrderDTO>>(SingletonDataBridge.GetInstance().Orders.OrderBy(o => o.Id).ToList());
		}

		public OrderDTO GetOrderById(int id)
		{
			return SingletonAutoMapper.GetInstance().Map<OrderDTO>(SingletonDataBridge.GetInstance().Orders.Where(o => o.Id == id).FirstOrDefault());
		}

		public List<OrderDetailDTO> GetOrderDetailsByOrderId(int id)
		{
			return SingletonAutoMapper.GetInstance().Map<List<OrderDetailDTO>>(SingletonDataBridge.GetInstance().Orderdetails.Where(od => od.OrderId==id).ToList());
		}

		public List<OrderDTO> GetOrdersByCustomerId(int id)
		{
			return SingletonAutoMapper.GetInstance().Map<List<OrderDTO>>(SingletonDataBridge.GetInstance().Orders.Where(o => o.CustomerId==id).ToList());
		}
	}
}
