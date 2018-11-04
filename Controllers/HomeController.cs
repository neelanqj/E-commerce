using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Models;
using Persistance;

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
            return View();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
