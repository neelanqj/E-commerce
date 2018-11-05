using System.Collections.Generic;

namespace E_commerce.Models
{
    public class OrderViewModel
    {
        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders {get;set;}

    }
}