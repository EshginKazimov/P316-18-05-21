using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontToBack.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly int _productsCount;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _productsCount = _dbContext.Products.Count();
        }

        public IActionResult Index()
        {
            ViewBag.ProductCount = _productsCount;

            //var products = _dbContext.Products.Include(x => x.Category).OrderByDescending(x => x.Id).Take(6).ToList();
            return View(/*products*/);
        }

        public IActionResult Detail(int id)
        {
            var product = _dbContext.Products.SingleOrDefault(x => x.Id == id);

            return View(product);
        }

        public IActionResult Load(int skip)
        {
            if (skip >= _productsCount)
            {
                return Content("Error");
            }

            var products = _dbContext.Products.Include(x => x.Category).OrderByDescending(x => x.Id).Skip(skip).Take(6).ToList();

            return PartialView("_ProductPartial", products);

            #region Old Version

            //var products = _dbContext.Products.Include(x => x.Category).Skip(6).Take(6).ToList();

            //return Json(products);

            #endregion
        }

        public IActionResult Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Content("Error");
            }

            var products = _dbContext.Products.Include(x => x.Category).OrderByDescending(x => x.Id)
                .Where(x => x.Name.ToLower().Contains(search.ToLower())).Take(10).ToList();

            return PartialView("_SearchProductPartial", products);
        }
    }
}
