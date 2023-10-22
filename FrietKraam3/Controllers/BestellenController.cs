using FrietKraam3.Data;
using FrietKraam3.Models;
using FrietKraam3.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace FrietKraam3.Controllers
{
    public class BestellenController : Controller
    {
        private readonly FrietKraamContext _Context;
        private readonly IHttpContextAccessor? _httpContextAccessor;



        public BestellenController(FrietKraamContext Context, IHttpContextAccessor httpContextAccessor)
        {
            _Context = Context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var products = _Context.Menus.ToList();
            var viewmodel = new BestellenViewModel
            {
                Products = products
            };

            return View(viewmodel);
        }


        public async Task<IActionResult> AddToCart(string ProductName, double ProductPrice, int ProductId)
        {
            List<Product>? Products;
            if (HttpContext.Session.TryGetValue("Cart", out byte[] ProductsData))
            {
                Products = JsonConvert.DeserializeObject<List<Product>>(Encoding.UTF8.GetString(ProductsData));
            }
            else
            {
                Products = new List<Product>();
            }

            Products.Add(new Product
            {
                ProductId = ProductId,

                ProductName = ProductName,

                ProductPrice = ProductPrice

            });

            HttpContext.Session.Set("Cart", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Products)));



            return RedirectToAction("Index", Products);
        }

        public async Task<IActionResult> ToDatabase()
        {
            List<Product> products;

            if (HttpContext.Session.TryGetValue("Cart", out byte[] productData))
            {
                products = JsonConvert.DeserializeObject<List<Product>>(Encoding.UTF8.GetString(productData));
            }
            else
            {
                products = new List<Product>();
            }

            // Create a new order and associate cart items with it
            var order = new Order
            {
                TotalPrice = products.Sum(p => p.ProductPrice), // Calculate the total price
                CustomerId = 1, // Set the customer ID as needed
                CartItems = products.Select(p => new CartItem
                {
                    Product = p,
                    Quantity = 1 // Set the quantity as needed
                }).ToList()
            };

            // Add the order to the database
            _Context.Orders.Add(order);
            await _Context.SaveChangesAsync();

            // Clear the session data
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index"); // Redirect to a relevant action

        }

        public async Task<IActionResult> ShowCart()
        {
            List<Product> products;

            if (HttpContext.Session.TryGetValue("Cart", out byte[] productData))
            {
                products = JsonConvert.DeserializeObject<List<Product>>(Encoding.UTF8.GetString(productData));
            }
            else
            {
                products = new List<Product>();
            }
            return View("ShowCart", products);
        }
    }

}







