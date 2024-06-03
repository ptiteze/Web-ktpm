namespace Kitchen_MVC.DTO.OrderDetail
{
	public class CreateOrderDetailRequest
	{
		public int OrderId { get; set; }

		public int ProductId { get; set; }

		public int Quantity { get; set; }

		public decimal Price { get; set; }
	}
}
