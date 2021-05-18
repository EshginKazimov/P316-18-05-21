using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontToBack.DataAccessLayer;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FrontToBack.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public HeaderViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.BasketCount = 0;
            var cookieBasket = Request.Cookies["basket"];
            if (!string.IsNullOrEmpty(cookieBasket))
            {
                var basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(cookieBasket);
                ViewBag.BasketCount = basketViewModels.Count;
            }

            var bio = await _dbContext.Bio.FirstOrDefaultAsync();

            return View(bio);
        }
    }
}
