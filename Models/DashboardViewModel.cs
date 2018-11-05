using System.Collections.Generic;

namespace E_commerce.Models
{
    public class DashboardViewModel
    {
        public List<Product> Products { get; set; }
        public List<Order>  Orders { get; set; }
        public List<Customer> Customers { get; set; }
    }
}