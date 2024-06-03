using AutoMapper;
using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.OrderDetail;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;

namespace Kitchen_MVC.Repositores
{
	public class OrderdetailRepository : IOrderdetailRepository
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public OrderdetailRepository(DataContext dataContext, IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}
		public async Task<bool> CreateOrderDetail(CreateOrderDetailRequest request)
		{
			try
			{
				var orderdetail = _mapper.Map<Orderdetail>(request);
				_dataContext.Add(orderdetail);
				_dataContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
