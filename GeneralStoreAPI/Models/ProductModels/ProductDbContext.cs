using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models.ProductModels
{
    public class ProductDbContext : DbContext 
    {
        public ProductDbContext() : base("DefaultConnection")
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}