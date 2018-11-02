using System;
using System.ComponentModel.DataAnnotations;

namespace E_commerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}