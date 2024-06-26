﻿namespace Kitchen_MVC.DTO.Order
{
	public class OrderDTO
	{
		public int Id { get; set; }
		public DateTime CreateAt { get; set; }

		public int CustomerId { get; set; }

		public int Status { get; set; }

		public bool PaymentStatus { get; set; }

		public int? EmployeeId { get; set; }
	}
}
