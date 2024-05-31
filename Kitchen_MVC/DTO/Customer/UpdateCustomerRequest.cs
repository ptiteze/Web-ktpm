namespace Kitchen_MVC.DTO.Customer
{
	public class UpdateCustomerRequest
	{
		public string Fullname { get; set; } = null!;

		public string? PhoneNumber { get; set; }

		public string? Address { get; set; }
	}
}
