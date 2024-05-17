using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Company.Data;
using Company.Models;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly CompanyContext _context;

        public ProductsController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return await _context.Product.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            Product? product = await _context.Product.FindAsync(id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(Product newProduct, int id)
        {
            if (id != newProduct.Id) return BadRequest();
            
            _context.Entry(newProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (await ProductExists(id)) throw;
                else return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ProductDelete(int id)
        {
            Product? product = await _context.Product.FindAsync(id);

            if (product == null) return NotFound();

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ProductExists(int id)
        {
            Product? product = await _context.Product.FindAsync(id);

            if (product == null) return false;

            return true;
        }
    }
}
