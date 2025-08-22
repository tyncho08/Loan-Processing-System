using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LPSystemWebAPICore.Data;
using LPSystemWebAPICore.Models;

namespace LPSystemWebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanTablesController : ControllerBase
    {
        private readonly LPSystemContext _context;

        public LoanTablesController(LPSystemContext context)
        {
            _context = context;
        }

        // GET: api/LoanTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanTable>>> GetLoanTables()
        {
            return await _context.LoanTables.ToListAsync();
        }

        // GET: api/LoanTables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoanTable>> GetLoanTable(int id)
        {
            var loanTable = await _context.LoanTables.FindAsync(id);

            if (loanTable == null)
            {
                return NotFound();
            }

            return loanTable;
        }

        // PUT: api/LoanTables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoanTable(int id, LoanTable loanTable)
        {
            if (id != loanTable.LoanId)
            {
                return BadRequest();
            }

            _context.Entry(loanTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanTableExists(id))
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

        // POST: api/LoanTables
        [HttpPost]
        public async Task<ActionResult<LoanTable>> PostLoanTable(LoanTable loanTable)
        {
            _context.LoanTables.Add(loanTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoanTable", new { id = loanTable.LoanId }, loanTable);
        }

        // DELETE: api/LoanTables/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoanTable(int id)
        {
            var loanTable = await _context.LoanTables.FindAsync(id);
            if (loanTable == null)
            {
                return NotFound();
            }

            _context.LoanTables.Remove(loanTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoanTableExists(int id)
        {
            return _context.LoanTables.Any(e => e.LoanId == id);
        }
    }
}