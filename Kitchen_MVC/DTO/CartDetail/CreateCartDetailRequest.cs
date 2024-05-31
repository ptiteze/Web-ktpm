namespace Kitchen_MVC.DTO.CartDetail
{
	public class CreateCartDetailRequest
	{
		public int CustomerId { get; set; }

		public int ProductId { get; set; }

		public int Quantity { get; set; } = 1;
	}
}
