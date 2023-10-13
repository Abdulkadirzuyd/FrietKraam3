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



            return RedirectToAction("Bestellen", Products);

        }

    }
}




      