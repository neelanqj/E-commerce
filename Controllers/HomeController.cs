﻿using System;
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

        public IActionResult Orders(){
            OrderViewModel vm = new OrderViewModel();
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
