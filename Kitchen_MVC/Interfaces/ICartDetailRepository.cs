using Kitchen_MVC.DTO.CartDetail;

namespace Kitchen_MVC.Interfaces
{
    public interface ICartDetailRepository
    {
        Task<bool> CreateCartDetail(CreateCartDetailRequest request);
        Task<bool> UpdateCartDetail(int IdProduct, int IdCustomer, UpdateCartDetailRequest request);
        Task<bool> DeleteCartDetail(int idProduct, int IdCustomer);
        Task<List<CartDetailDTO>> GetCartDetailByIdCustomer(int id);
    }
}
