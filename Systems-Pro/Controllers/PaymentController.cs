using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Systems_Pro.Models;

namespace Systems_Pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly TransactionDbContext _context;

        public PaymentController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentModel>>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

        // GET: api/Payment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentModel>> GetPaymentModel(int id)
        {
            var paymentModel = await _context.PaymentDetails.FindAsync(id);

            if (paymentModel == null)
            {
                return NotFound();
            }

            return paymentModel;
        }

        // PUT: api/Payment/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentModel(int id, PaymentModel paymentModel)
        {
            if (id != paymentModel.PaymentDetailId)
            {
                return BadRequest();
            }

            _context.Entry(paymentModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Payment
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PaymentModel>> PostPaymentModel(PaymentModel paymentModel)
        {
            _context.PaymentDetails.Add(paymentModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentModel", new { id = paymentModel.PaymentDetailId }, paymentModel);
        }

        // DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentModel>> DeletePaymentModel(int id)
        {
            var paymentModel = await _context.PaymentDetails.FindAsync(id);
            if (paymentModel == null)
            {
                return NotFound();
            }

            _context.PaymentDetails.Remove(paymentModel);
            await _context.SaveChangesAsync();

            return paymentModel;
        }

        private bool PaymentModelExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.PaymentDetailId == id);
        }
    }
}
