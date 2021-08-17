using GeneralStoreAPI.Models;
using GeneralStoreAPI.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //POST (Create)
        //api/Products
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Product product)
        {
            if(product is null)
            {
                return BadRequest("Request Body Cannot Be Empty");
            }
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                if(await _context.SaveChangesAsync() == 1)
                {
                    return Ok("Product Created!");
                }
            }
            return BadRequest(ModelState);
        }

        //GET ALL
        //api/Product
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        //GET BY SKU
        //api/Products/{Sku}
        [HttpGet]
        public async Task<IHttpActionResult> GetBySku([FromUri] string sku)
        {
            Product products = await _context.Products.FindAsync(sku);
            
            if(products != null)
            {
                return Ok(products);
            }
            return NotFound();
        }
    }
}
