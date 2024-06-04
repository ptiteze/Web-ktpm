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

		public bool CartExists(int idProduct, int IdCustomer)
		{
			var cartDetail = _context.CartDetails.Where(c => c.ProductId == idProduct && c.CustomerId == IdCustomer).FirstOrDefault();
			if (cartDetail == null) return false;
			return true;
		}

		public async Task<bool> CreateCartDetail(CreateCartDetailRequest request)
		{
			CartDetail cartDetail = _mapper.Map<CartDetail>(request);
			_context.Add(cartDetail);
			_context.SaveChanges();
			return true;
		}

		public async Task<bool> DeleteCartDetail(int idProduct, int idCustomer)
		{
			var cartDetail = _context.CartDetails.Where(c => c.ProductId == idProduct && c.CustomerId == idCustomer).FirstOrDefault();
			if (cartDetail == null) return false;
			_context.Remove(cartDetail);
			_context.SaveChanges();
			return true;
		}

		public int GetCartCount(int IdCustomer)
		{
			return _context.CartDetails.Count(c => c.CustomerId == IdCustomer);
		}

		//public async Task<List<CartDetailDTO>> GetCartDetailByIdCustomer(int id)
		//{
		//	var cartDetails = _context.CartDetails.Where(c => c.CustomerId == id).ToList();
		//	var cartDetailDTOs = new List<CartDetailDTO>();
		//	cartDetails.ForEach(x => cartDetailDTOs.Add(_mapper.Map<CartDetailDTO>(x)));
		//	return cartDetailDTOs;
		//}

		public async Task<bool> UpdateCartDetail(int IdProduct, int IdCustomer, int Quantity)
		{
			var cartDetail = _context.CartDetails.Where(c => c.ProductId == IdProduct && c.CustomerId == IdCustomer).FirstOrDefault();
			if (cartDetail == null) return false;
			cartDetail.Quantity = Quantity;
			_context.Update(cartDetail);
			return true;
		}
	}
}
