using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Models;
using Persistance;
using Microsoft.EntityFrameworkCore;

namespace E_commerce.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext _dbContext;
        public HomeController(MyDbContext context){
            _dbContext = context;
        }

        public IActionResult Index()
        {
            DashboardViewModel vm = new DashboardViewModel();

            vm.Products = _dbContext.Products.Take(6).ToList();
            vm.Customers = _dbContext.Customers.Take(3).ToList();
            vm.Orders = _dbContext.Orders.Include(o=>o.Customer).Include(p=>p.Product).Take(3).ToList();

            return View(vm);
        }

        [HttpGet]
        [Route("/customers")]
        public IActionResult Customers(){
            List<Customer> customers = _dbContext.Customers.ToList();
            return View(customers);
        }

        [HttpPost]
        [Route("/customers/add")]
        public IActionResult AddCustomer(Customer customer){
            _dbContext.Add(customer);
            _dbContext.SaveChanges();

            return Redirect("/customers");
        }

        [HttpGet]
        [Route("/products")]
        public IActionResult Products(){
            List<Product> products = _dbContext.Products.Take(12).ToList();
            return View(products);
        }

        [HttpPost]
        [Route("/products")]
        public IActionResult Products(string filter = ""){
            List<Product> products;
            
            if (filter != "") {
                products =_dbContext.Products.Where(p=>p.Name.Contains(filter)).ToList();
            } else {
                products =_dbContext.Products.ToList();
            }
            return View(products);
        }

        [HttpPost]
        [Route("/products/add")]
        public IActionResult AddProducts(Product product){
            _dbContext.Add(product);
            _dbContext.SaveChanges();

            return Redirect("/products");
        }

        [HttpGet]
        [Route("/orders")]
        public IActionResult Orders(){
            OrderViewModel vm = new OrderViewModel();

            vm.Customers = _dbContext.Customers.ToList();
            vm.Orders = _dbContext.Orders.ToList();
            vm.Products = _dbContext.Products.Where(p => p.Quantity > 0).ToList();

            return View(vm);
        }

        [HttpPost]
        [Route("/orders")]
        public IActionResult Orders(Order order){
            if(_dbContext.Products.Any(p => p.ProductId == order.ProductId && p.Quantity > order.Quantity)) {
                Product product = _dbContext.Products.Where(p => p.ProductId == order.ProductId && p.Quantity > order.Quantity).FirstOrDefault();
                product.Quantity = product.Quantity - order.Quantity;
                _dbContext.Add(order);
                _dbContext.SaveChanges();
            } else {
                Product product = _dbContext.Products.Where(p => p.ProductId == order.ProductId).FirstOrDefault();

                ModelState.AddModelError("Quantity","There are only " +product.Quantity + " of " +product.Name+ " left, but you ordered " + order.Quantity + " products.");
            }
            
            OrderViewModel vm = new OrderViewModel();

            vm.Customers = _dbContext.Customers.ToList();
            vm.Orders = _dbContext.Orders.ToList();
            vm.Products = _dbContext.Products.Where(p => p.Quantity > 0).ToList();

            return View(vm);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
