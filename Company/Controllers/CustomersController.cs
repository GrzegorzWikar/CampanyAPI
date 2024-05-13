using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Company.Data;
using Company.Models;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly CompanyContext _context;

        public CustomersController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: api/customer
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            return await _context.Customer.ToListAsync();
        }


        [HttpGet("{id}")]
        // GET /api/customer/{id}
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            Customer? customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        // Post: api/customer
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateCustomer), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> UpdateCustomer(Customer newCustomer, int id)
        // Put: api/customer/{id}
        {
            if (id != newCustomer.Id)
            {
                return BadRequest();
            }

            _context.Entry(newCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await CustomerExists(id))
                {
                    throw;
                }
                else
                {
                    return NotFound();
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> CustomerDelete(int id)
        {
            Customer? customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CustomerExists(int id)
        {
            Customer? customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return false;
            }

            return true;
        }
    }
}
