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
            var products = _Context.Products.ToList();
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
        public async Task<IActionResult> DeleteCart(int ProductId)
        {
            // initieer List
            List<Product>? Products;

            //Check of er al iets in de cart zit
            if (HttpContext.Session.TryGetValue("Cart", out byte[] ProductsData))
            {
                Products = JsonConvert.DeserializeObject<List<Product>>(Encoding.UTF8.GetString(ProductsData));
            }
            //Zo niet, maak Cart aan.
            else
            {
                Products = new List<Product>();
            }

            //initeer variabele om te verwijderen
            var remove = Products.FirstOrDefault(r => r.ProductId == ProductId);

            //check of dit item bestaat (niet null is)
            if (remove != null)
            {
                //verwijder item uit de storage
                Products.Remove(remove);
                //update
                HttpContext.Session.Set("Cart", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(Products)));
            }

            return RedirectToAction("Index");

        }
        public async Task<IActionResult> ToDatabase()
        {
            var username = "Abdul";
            var customer = _Context.Customers.FirstOrDefault(c => c.CustomerName == username);
            List<Product> products;

            if (HttpContext.Session.TryGetValue("Cart", out byte[] productData))
            {
                products = JsonConvert.DeserializeObject<List<Product>>(Encoding.UTF8.GetString(productData));
            }
            else
            {
                products = new List<Product>();
            }


            var order = new Order
            {
                TotalPrice = products.Sum(p => p.ProductPrice),
                CustomerId = customer.CustomerId,
            };

            _Context.Orders.Add(order);
            await _Context.SaveChangesAsync();


            foreach (var product in products)
            {
                var cartItem = new CartItem
                {
                    ProductId = product.ProductId,
                    OrderId = order.OrderId,
                    Quantity = 1,
                };

                _Context.CartItems.Add(cartItem);
            }

            await _Context.SaveChangesAsync();

            HttpContext.Session.Remove("Cart");

            return View();
        }
    }

}







