using Instiute01.Data;
using Instiute01.Dto;
using Instiute01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instiute01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BatchController(ApplicationDbContext context)
        {
            _context = context;
        }
       // [Authorize(Roles = "Admin,Teacher")]
        // GET: api/Batch
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatchDTO>>> GetBatches()
        {
            var batches = await _context.Batches
                .Select(b => new BatchDTO
                {
                    BatchId = b.BatchId,
                    BatchName = b.BatchName,
                    StartDate = b.StartDate,
                    StartYear = b.StartYear,
                    EndYear = b.EndYear,
                    CourseId = b.CourseId
                })
                .ToListAsync();

            return Ok(batches);
        }
      //  [Authorize(Roles = "Admin")]
        // GET: api/Batch/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BatchDTO>> GetBatch(int id)
        {
            var batch = await _context.Batches
                .Where(b => b.BatchId == id)
                .Select(b => new BatchDTO
                {
                    BatchId = b.BatchId,
                    BatchName = b.BatchName,
                    StartDate = b.StartDate,
                    StartYear = b.StartYear,
                    EndYear = b.EndYear,
                    CourseId = b.CourseId
                })
                .FirstOrDefaultAsync();

            if (batch == null)
            {
                return NotFound();
            }

            return Ok(batch);
        }
       // [Authorize(Roles = "Admin")]
        // POST: api/Batch
        [HttpPost]
        public async Task<ActionResult<BatchDTO>> PostBatch(BatchDTO batchDTO)
        {
            var batch = new Batch
            {
                BatchName = batchDTO.BatchName,
                StartDate = batchDTO.StartDate,
                StartYear = batchDTO.StartYear,
                EndYear = batchDTO.EndYear,
                CourseId = batchDTO.CourseId
            };

            _context.Batches.Add(batch);
            await _context.SaveChangesAsync();

            batchDTO.BatchId = batch.BatchId;

            return CreatedAtAction(nameof(GetBatch), new { id = batch.BatchId }, batchDTO);
        }
       // [Authorize(Roles = "Admin")]
        // PUT: api/Batch/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatch(int id, BatchDTO batchDTO)
        {
            if (id != batchDTO.BatchId)
            {
                return BadRequest();
            }

            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            batch.BatchName = batchDTO.BatchName;
            batch.StartDate = batchDTO.StartDate;
            batch.StartYear = batchDTO.StartYear;
            batch.EndYear = batchDTO.EndYear;
            batch.CourseId = batchDTO.CourseId;

            _context.Entry(batch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatchExists(id))
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
       // [Authorize(Roles = "Admin")]
        // DELETE: api/Batch/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatch(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            _context.Batches.Remove(batch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BatchExists(int id)
        {
            return _context.Batches.Any(e => e.BatchId == id);
        }
    }
}

