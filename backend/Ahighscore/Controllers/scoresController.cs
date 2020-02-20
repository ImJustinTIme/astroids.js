using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ahighscore.Models;


namespace Ahighscore.Controllers
{   
    
    [Route("api/[controller]")]
    [ApiController]
    public class scoresController : ControllerBase
    {
        private readonly scoreContext _context;

        public scoresController(scoreContext context)
        {
            _context = context;
        }

        // GET: api/scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<scoreItem>>> GetscoreItems()
        {
            return await _context.scoreItems.ToListAsync();
        }

        // GET: api/scores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<scoreItem>> GetscoreItem(long id)
        {
            var scoreItem = await _context.scoreItems.FindAsync(id);

            if (scoreItem == null)
            {
                return NotFound();
            }

            return scoreItem;
        }

        [HttpGet("sorted")]
        public async Task<ActionResult<IEnumerable<scoreItem>>> GetSortedScores() {
            var scoreItems = from s in _context.scoreItems select s;
            scoreItems = scoreItems.OrderByDescending( s => s.Score);
            return await scoreItems.ToListAsync();
        }

        // PUT: api/scores/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutscoreItem(long id, scoreItem scoreItem)
        {
            if (id != scoreItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(scoreItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!scoreItemExists(id))
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

        // POST: api/scores
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<scoreItem>> PostscoreItem(scoreItem scoreItem)
        {
            _context.scoreItems.Add(scoreItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetscoreItem", new { id = scoreItem.Id }, scoreItem);
        }

        // DELETE: api/scores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<scoreItem>> DeletescoreItem(long id)
        {
            var scoreItem = await _context.scoreItems.FindAsync(id);
            if (scoreItem == null)
            {
                return NotFound();
            }

            _context.scoreItems.Remove(scoreItem);
            await _context.SaveChangesAsync();

            return scoreItem;
        }

        private bool scoreItemExists(long id)
        {
            return _context.scoreItems.Any(e => e.Id == id);
        }
    }
}
