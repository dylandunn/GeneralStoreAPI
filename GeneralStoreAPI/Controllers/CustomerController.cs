using GeneralStoreAPI.Models;
using GeneralStoreAPI.Models.CustomerModels;
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
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // POST (Create)
        //api/Customer
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Customer customer)
        {
            if (customer is null)
            {
                return BadRequest("Request Body cannot be empty");
            }
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                if(await _context.SaveChangesAsync()== 1)
                {
                    return Ok("Customer Created");
                }
            }
            return BadRequest(ModelState);
        }

        //GET ALL 
        //api/Customer 
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        //GET BY ID
        // api/Customer/{id}
        [HttpGet]
        public async Task<IHttpActionResult> GetByID([FromUri] int id)
        {
            Customer customers = await _context.Customers.FindAsync(id);

            if (customers != null)
            {
                return Ok(customers);
            }
            return NotFound();
        }

        //PUT (update)
        // api/Customer/{id}
        [HttpPut]
        public async Task<IHttpActionResult> UpdateCustomer([FromUri] int id, [FromBody] Customer updatedCustomer)
        {
            if (id != updatedCustomer?.Id)
            {
                return BadRequest("ID's do not match.");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Customer customer = await _context.Customers.FindAsync(id);

            if (customer is null)
                return NotFound();

            customer.FirstName = updatedCustomer.FirstName;
            customer.LastName = updatedCustomer.LastName;
            //CheckBackForFullName

            await _context.SaveChangesAsync();
            return Ok("Customer was update!");
        }
        //DELETE
        //api/Customer/{id}
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCustomer([FromUri] int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if (customer is null)
                return NotFound();

            _context.Customers.Remove(customer);

            if(await _context.SaveChangesAsync() == 1 )
            {
                return Ok("Customer Was Deleted!");
            }
            return InternalServerError();
        }
    }
}
