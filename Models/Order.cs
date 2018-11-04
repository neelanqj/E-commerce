using System;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId {get;set;}
        public Customer Customer {get;set;}
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}