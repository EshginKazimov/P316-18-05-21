using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontToBack.DataAccessLayer;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //HttpContext.Session.SetString("demo", "Test");
            //Response.Cookies.Append("demo1", "Test1");

            var slider = _dbContext.Slider.FirstOrDefault();
            var sliderImages = _dbContext.SliderImages.ToList();
            var categories = _dbContext.Categories.ToList();
            //var products = _dbContext.Products.Include(x => x.Category).ToList();
            var about = _dbContext.About.FirstOrDefault();
            var aboutPolicies = _dbContext.AboutPolicies.ToList();

            var homeViewModel = new HomeViewModel
            {
                Slider = slider,
                SliderImages = sliderImages,
                Categories = categories,
                //Products = products,
                About = about,
                AboutPolicies = aboutPolicies
            };

            return View(homeViewModel);
        }

        public IActionResult AddToBasket(int? id)
        {
            if (id == null)
                return NotFound();

            var product = _dbContext.Products.Find(id);

            if (product == null)
                return NotFound();

            List<BasketViewModel> productsList;

            var basketCookie = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(basketCookie))
            {
                productsList = new List<BasketViewModel>();
            }
            else
            {
                productsList = JsonConvert.DeserializeObject<List<BasketViewModel>>(basketCookie);
            }

            var existProduct = productsList.FirstOrDefault(x => x.Id == id);
            if (existProduct == null)
            {
                productsList.Add(new BasketViewModel { Id = product.Id });
            }
            else
            {
                existProduct.Count++;
            }

            var productJson = JsonConvert.SerializeObject(productsList);
            Response.Cookies.Append("basket", productJson);

            return RedirectToAction("Index");
        }

        public IActionResult Basket()
        {
            //var session = HttpContext.Session.GetString("demo");
            //var cookie = Request.Cookies["demo1"];

            var cookieBasket = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(cookieBasket))
                return Content("No data found in basket");

            var basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(cookieBasket);
            var result = new List<BasketViewModel>();
            foreach (var basketViewModel in basketViewModels)
            {
                var dbProduct = _dbContext.Products.Find(basketViewModel.Id);
                if (dbProduct == null)
                    continue;

                basketViewModel.Price = dbProduct.Price;
                basketViewModel.Image = dbProduct.Image;
                basketViewModel.Name = dbProduct.Name;
                result.Add(basketViewModel);
            }

            var basket = JsonConvert.SerializeObject(result);
            Response.Cookies.Append("basket", basket);

            return Json(basket);
        }
    }
}
