using FrietKraam3.Data;
using FrietKraam3.Models;
using FrietKraam3.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrietKraam3.Controllers
{
    public class BestellenController : Controller
    {
        private readonly FrietKraamContext _Context;

        public BestellenController(FrietKraamContext Context)
        {
            _Context = Context; 
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
        public async Task<IActionResult> Bestellen()
        {
            return View();
        }



    }
}