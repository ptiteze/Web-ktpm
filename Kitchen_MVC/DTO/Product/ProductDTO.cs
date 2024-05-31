﻿namespace Kitchen_MVC.DTO.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? Status { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set;}
    }
}
