namespace Kitchen_MVC.DTO.Account
{
	public class InfoAccountDTO
	{
		public int Id { get; set; }

		public string Fullname { get; set; } = null!;

		public string Email { get; set; } = null!;

		public int RoleId { get; set; } 
	}
}
