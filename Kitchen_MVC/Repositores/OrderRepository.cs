using AutoMapper;
using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.Category;
using Kitchen_MVC.DTO.Order;
using Kitchen_MVC.DTO.OrderDetail;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;

namespace Kitchen_MVC.Repositores
{
	public class OrderRepository : IOrderRepository
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public OrderRepository(DataContext dataContext, IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}
		public async Task<int> CreateOrder(CreateOrderRequest request)
		{
			try
			{
				var order = _mapper.Map<Order>(request);
				_dataContext.Add(order);
				_dataContext.SaveChanges();
				return order.Id;
			}catch(Exception ex)
			{
				return 0;
			}
		}

		public List<OrderDTO> GetAllOrders()
		{
			return _mapper.Map<List<OrderDTO>>(_dataContext.Orders.OrderBy(o => o.Id).ToList());
		}

		public OrderDTO GetOrderById(int id)
		{
			return _mapper.Map<OrderDTO>(_dataContext.Orders.Where(o => o.Id == id).FirstOrDefault());
		}

		public List<OrderDetailDTO> GetOrderDetailsByOrderId(int id)
		{
			return _mapper.Map<List<OrderDetailDTO>>(_dataContext.Orderdetails.Where(od => od.OrderId==id).ToList());
		}

		public List<OrderDTO> GetOrdersByCustomerId(int id)
		{
			return _mapper.Map<List<OrderDTO>>(_dataContext.Orders.Where(o => o.CustomerId==id).ToList());
		}
	}
}
