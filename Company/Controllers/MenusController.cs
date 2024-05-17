using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Company.Data;
using Company.Models;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/[cotroller]")]
    public class MenusController : Controller
    {
        private readonly CompanyContext _context;

        public MenusController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Menu>>> GetAllMenus()
        {
            return await _context.Menu.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            Menu? menu = await _context.Menu.FindAsync(id);

            if (menu == null) return NotFound();

            return Ok(menu);
        }

        [HttpPost]
        public async Task<ActionResult<Menu>> CreateMenu(Menu menu)
        {
            _context.Menu.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateMenu), new { id = menu.Id }, menu);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Menu>> UpdateMenu(Menu newMenu, int id)
        {
            if (id != newMenu.Id) return BadRequest();

            _context.Entry(newMenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await MenuExists(id)) throw;
                else return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> MenuDelete(int id)
        {
            Menu? menu = await _context.Menu.FindAsync(id);

            if (menu == null) return NotFound();

            _context.Menu.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public async Task<bool> MenuExists(int id)
        {
            Menu? menu = await _context.Menu.FindAsync(id);

            if (menu == null) return false;

            return true;
        }
    }
}
