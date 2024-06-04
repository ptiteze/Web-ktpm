using Kitchen_MVC.DTO.OrderDetail;

namespace Kitchen_MVC.Interfaces
{
    public interface IOrderdetailRepository
    {
        Task<bool> CreateOrderDetail(CreateOrderDetailRequest request);

    }
}
