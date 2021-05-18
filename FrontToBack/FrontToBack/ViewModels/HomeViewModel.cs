using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontToBack.Models;

namespace FrontToBack.ViewModels
{
    public class HomeViewModel
    {
        public Slider Slider { get; set; }

        public List<SliderImage> SliderImages { get; set; }
        
        public List<Category> Categories { get; set; }
     
        //public List<Product> Products { get; set; }

        public About About { get; set; }

        public List<AboutPolicy> AboutPolicies { get; set; }
    }
}
