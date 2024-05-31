using AutoMapper;
using Kitchen_MVC.Data;
using Kitchen_MVC.DTO.CartDetail;
using Kitchen_MVC.Interfaces;
using Kitchen_MVC.Models;

namespace Kitchen_MVC.Repositores
{
	public class CartDetailRepository : ICartDetailRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;
		public CartDetailRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<bool> CreateCartDetail(CreateCartDetailRequest request)
		{
			CartDetail cartDetail = _mapper.Map<CartDetail>(request);
			_context.Add(cartDetail);
			_context.SaveChanges();
			return true;
		}

		public Task<bool> DeleteCartDetail(int idProduct, int IdCustomer)
		{
			throw new NotImplementedException();
		}

		public async Task<List<CartDetailDTO>> GetCartDetailByIdCustomer(int id)
		{
			var cartDetails = _context.CartDetails.Where(c => c.CustomerId == id).ToList();
			var cartDetailDTOs = new List<CartDetailDTO>();
			cartDetails.ForEach(x => cartDetailDTOs.Add(_mapper.Map<CartDetailDTO>(x)));
			return cartDetailDTOs;
		}

		public Task<bool> UpdateCartDetail(int IdProduct, int IdCustomer, UpdateCartDetailRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
