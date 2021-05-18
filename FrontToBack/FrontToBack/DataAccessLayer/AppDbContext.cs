using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontToBack.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Slider> Slider { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<AboutPolicy> AboutPolicies { get; set; }
        public DbSet<Bio> Bio { get; set; }
    }
}
