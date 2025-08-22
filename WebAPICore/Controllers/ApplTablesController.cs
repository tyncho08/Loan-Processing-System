using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LPSystemWebAPICore.Data;
using LPSystemWebAPICore.Models;

namespace LPSystemWebAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplTablesController : ControllerBase
    {
        private readonly LPSystemContext _context;

        public ApplTablesController(LPSystemContext context)
        {
            _context = context;
        }

        // GET: api/ApplTables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplTable>>> GetApplTables()
        {
            return await _context.ApplTables
                .Include(a => a.User)
                .Include(a => a.Loan)
                .ToListAsync();
        }

        // GET: api/ApplTables/1/2/3 (AppId/UserId/LoanId)
        [HttpGet("{appId}/{userId}/{loanId}")]
        public async Task<ActionResult<ApplTable>> GetApplTable(int appId, int userId, int loanId)
        {
            var applTable = await _context.ApplTables
                .Include(a => a.User)
                .Include(a => a.Loan)
                .FirstOrDefaultAsync(a => a.AppId == appId && a.UserId == userId && a.LoanId == loanId);

            if (applTable == null)
            {
                return NotFound();
            }

            return applTable;
        }

        // PUT: api/ApplTables/1/2/3
        [HttpPut("{appId}/{userId}/{loanId}")]
        public async Task<IActionResult> PutApplTable(int appId, int userId, int loanId, ApplTable applTable)
        {
            if (appId != applTable.AppId || userId != applTable.UserId || loanId != applTable.LoanId)
            {
                return BadRequest();
            }

            _context.Entry(applTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplTableExists(appId, userId, loanId))
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

        // POST: api/ApplTables
        [HttpPost]
        public async Task<ActionResult<ApplTable>> PostApplTable(ApplTable applTable)
        {
            _context.ApplTables.Add(applTable);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApplTableExists(applTable.AppId, applTable.UserId, applTable.LoanId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApplTable", 
                new { appId = applTable.AppId, userId = applTable.UserId, loanId = applTable.LoanId }, 
                applTable);
        }

        // DELETE: api/ApplTables/1/2/3
        [HttpDelete("{appId}/{userId}/{loanId}")]
        public async Task<IActionResult> DeleteApplTable(int appId, int userId, int loanId)
        {
            var applTable = await _context.ApplTables
                .FirstOrDefaultAsync(a => a.AppId == appId && a.UserId == userId && a.LoanId == loanId);
            
            if (applTable == null)
            {
                return NotFound();
            }

            _context.ApplTables.Remove(applTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplTableExists(int appId, int userId, int loanId)
        {
            return _context.ApplTables.Any(e => e.AppId == appId && e.UserId == userId && e.LoanId == loanId);
        }
    }
}