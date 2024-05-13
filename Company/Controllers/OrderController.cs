using Company.Data;
using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly CompanyContext _context;

        public OrderController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: api/order
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            return await _context.Order.ToListAsync();
        }

        
        [HttpGet("{id}")]
        // GET /api/order/{id}
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            Order? order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        // Post: api/order
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(nameof(CreateOrder),new {id = order.Id  }, order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(Order newOrder, int id)
        // Put: api/order/{id}
        {
            if (id != newOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(newOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await OrderExists(id))
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
        public async Task<ActionResult> OrderDelete(int id)
        {
            Order? order = await _context.Order.FindAsync(id);

            if(order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> OrderExists(int id)
        {
            Order? order = await _context.Order.FindAsync(id);

            if (order == null)
            {
                return false;
            }

            return true;
        }
    }
}
