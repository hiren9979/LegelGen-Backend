using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LegalGen_Backend.Models;
using LegalGen_Backend.DBContext;

namespace LegalGenApi.Controllers
{
    [Route("LegelGen/ResearchBooks")]
    [ApiController]
    public class ResearchBooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ResearchBooksController(ApplicationDbContext context)
        {

            _context = context;

        }

        // GET: api/ResearchBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResearchBook>>> GetResearchBooks()
        {
            Console.WriteLine(_context.ResearchBooks);
            return await _context.ResearchBooks.ToListAsync();
        }

        // GET: api/ResearchBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResearchBook>> GetResearchBook(int id)
        {
            var researchBook = await _context.ResearchBooks.FindAsync(id);
            //       
            if (researchBook == null)
            {
                return NotFound();
            }

            return researchBook;
        }

        // PUT: api/ResearchBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResearchBook(int id, ResearchBook researchBook)
        {
            if (id != researchBook.Id)
            {
                return BadRequest();
            }

            _context.Entry(researchBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResearchBookExists(id))
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

        [HttpGet("GetResearchBookByLegalInfo")]
        public async Task<ActionResult<ResearchBook>> GetResearchBookByLegalInfo(int researchBookId, string section, string caseType, string CaseNumber, string Citation, string Act, string Petitioner, string Respondent)
        {
            try
            {
                var researchBook = await _context.ResearchBooks
                    .Where(rb => rb.Id == researchBookId &&
                                 (string.IsNullOrEmpty(section) || rb.Name.Contains(section)) ||
                                 (string.IsNullOrEmpty(caseType) || rb.Name.Contains(caseType)) ||
                                  (string.IsNullOrEmpty(CaseNumber) || rb.Name.Contains(CaseNumber)) ||
                                   (string.IsNullOrEmpty(Citation) || rb.Name.Contains(Citation)) ||
                                    (string.IsNullOrEmpty(Act) || rb.Name.Contains(Act)) ||
                                     (string.IsNullOrEmpty(Petitioner) || rb.Name.Contains(Petitioner)) ||
                                      (string.IsNullOrEmpty(Respondent) || rb.Name.Contains(Respondent))
                                 )
                    .FirstOrDefaultAsync();
                if (researchBook == null)
                {
                    return NotFound("No matching ResearchBook found.");
                }
                return researchBook;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/ResearchBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("createResearchBook")]
        public async Task<ActionResult<ResearchBook>> PostResearchBook(ResearchBook researchBook)
        {
            Console.WriteLine(researchBook);
            _context.ResearchBooks.Add(researchBook);
            await _context.SaveChangesAsync();

            return Ok(researchBook);
        }

        // DELETE: api/ResearchBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResearchBook(int id)
        {
            var researchBook = await _context.ResearchBooks.FindAsync(id);
            if (researchBook == null)
            {
                return NotFound();
            }

            _context.ResearchBooks.Remove(researchBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/ResearchBooks/GetResearchBooksByUserId/5
        [HttpGet("GetResearchBooksByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<ResearchBook>>> GetResearchBooksByUserId(string userId)
        {
            var researchBooks = await _context.ResearchBooks
                .Where(rb => rb.UserId == userId)
                .ToListAsync();

            if (researchBooks == null)
            {
                return NotFound();
            }

            return researchBooks;
        }


        private bool ResearchBookExists(int id)
        {
            return _context.ResearchBooks.Any(e => e.Id == id);
        }
    }
}
