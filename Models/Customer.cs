using System;

namespace E_commerce.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}