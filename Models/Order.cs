using System;

namespace E_commerce.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId {get;set;}
        public Customer Customer {get;set;}
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}