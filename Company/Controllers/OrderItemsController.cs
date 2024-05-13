using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Company.Data;
using Company.Models;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemsController : Controller
    {
        private readonly CompanyContext _context;

        public OrderItemsController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: OrderItems
        public async Task<List<OrderItem>> GetAllOrderItems()
        {
            return await _context.OrderItem.ToListAsync();
        }

        [HttpGet("{id}")]
        // GET: OrderItems/Details/5
        public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
        {
           OrderItem? orderItem = await _context.OrderItem.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        // POST: api/OrderItems
        public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
        {
            _context.OrderItem.Add(orderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateOrderItem),new { id = orderItem.Id}, orderItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderItem>> UpdateOrderItem(OrderItem orderItem, int id)
        {
            if (id != orderItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (await OrderItemExists(id))
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

        private async Task<bool> OrderItemExists(int id)
        {
            OrderItem? orderItem = await _context.OrderItem.FindAsync(id);

            if (orderItem == null) return false;

            return true;
        }

    }
}
