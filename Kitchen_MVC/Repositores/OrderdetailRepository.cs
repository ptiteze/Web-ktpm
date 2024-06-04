using AutoMapper;
using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.OrderDetail;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;
using Kitchen_MVC.Singleton;

namespace Kitchen_MVC.Repositores
{
	public class OrderdetailRepository : IOrderdetailRepository
	{
		//private readonly DataContext SingletonDataBridge.GetInstance();
		//private readonly IMapper SingletonAutoMapper.GetInstance();
		public OrderdetailRepository(/*DataContext dataContext, IMapper mapper*/)
		{
			//SingletonDataBridge.GetInstance() = dataContext;
			//SingletonAutoMapper.GetInstance() = mapper;
		}
		public async Task<bool> CreateOrderDetail(CreateOrderDetailRequest request)
		{
			try
			{
				var orderdetail = SingletonAutoMapper.GetInstance().Map<Orderdetail>(request);
				SingletonDataBridge.GetInstance().Add(orderdetail);
				SingletonDataBridge.GetInstance().SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
