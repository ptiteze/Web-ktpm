namespace Kitchen_MVC.DTO.Customer
{
	public class CreateCustomerRequest
	{
		public string Fullname { get; set; } = null!;

		public string? PhoneNumber { get; set; }

		public string? Address { get; set; }

		public string Email { get; set; } = null!;
		public string Password { get; set; }
	}
}
