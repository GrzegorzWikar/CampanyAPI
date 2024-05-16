using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Company.Data;
using Company.Models;
using System.Data;

namespace Company.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : Controller
    {
        private readonly CompanyContext _context;

        public PaymentsController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetAllPayments()
        {
            return await _context.Payment.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            Payment? payment = await _context.Payment.FindAsync(id);


            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        [HttpPost]
        // GET: Payments/Create
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreatePayment), new { id = payment.Id }, payment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Payment>> UpdatePayment(Payment newPayment, int id)
        {
            if (id != newPayment.Id) 
            {
                return BadRequest();
            }

            _context.Entry(newPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DBConcurrencyException) 
            {
                if(await PaymentExists(id))
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
        public async Task<ActionResult> PaymentDelete(int id)
        {
            Payment? payment = await _context.Payment.FindAsync(id);

            if (payment == null)  return NotFound();

            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PaymentExists(int id)
        {
            Payment? payment = await _context.Payment.FindAsync(id);

            if (payment == null) return false;

            return true;
        }
    }
}
