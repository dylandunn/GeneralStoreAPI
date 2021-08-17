using GeneralStoreAPI.Models;
using GeneralStoreAPI.Models.ProductModels;
using GeneralStoreAPI.Models.TransactionModels;
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
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        //POST
        //api/Transactions
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Transaction transaction)
        {
            if(transaction is null)
            {
                return BadRequest("Request Body cannot be empty");
            }
            var customer = await _context.Customers.FindAsync(transaction.CustomerId);
            var product = await _context.Products.FindAsync(transaction.Sku);
            if(product is null)
            {
                return BadRequest($"The Target Product with the Sku of {product.Sku} Does Not Exist. ");
            }
            if(product.IsInStock || transaction.ItemCount <= product.NumberInInventory || ModelState.IsValid)
            {
                    _context.Transactions.Add(transaction);
                    _context.Products.Remove(product);
                    if(await _context.SaveChangesAsync() == 2)
                {
                    return Ok("Transaction Was Completed!");
                }
            }
            return BadRequest(ModelState);
            
        }
        
        //GET ALL
        //api/Transactions
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Transaction> transactions = await _context.Transactions.ToListAsync();
            return Ok(transactions);
        }

        //GET BY ID
        //Api/Transaction
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Transaction transaction = await _context.Transactions.FindAsync(id);
            if(transaction != null)
            {
                return Ok(transaction);
            }
            return NotFound();
        }
    }
}
