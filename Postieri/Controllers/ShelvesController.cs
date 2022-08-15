﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelvesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ShelvesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Shelves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shelf>>> GetShelves()
        {
          if (_context.Shelves == null)
          {
              return NotFound();
          }
            return await _context.Shelves.ToListAsync();
        }

        // GET: api/Shelves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shelf>> GetShelf(int id)
        {
          if (_context.Shelves == null)
          {
              return NotFound();
          }
            var shelf = await _context.Shelves.FindAsync(id);

            if (shelf == null)
            {
                return NotFound();
            }

            return shelf;
        }

        // PUT: api/Shelves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShelf(int id, Shelf shelf)
        {
            if (id != shelf.ShelfId)
            {
                return BadRequest();
            }

            _context.Entry(shelf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShelfExists(id))
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

        // POST: api/Shelves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shelf>> PostShelf(Shelf shelf)
        {
          if (_context.Shelves == null)
          {
              return Problem("Entity set 'AppDbContext.Shelves'  is null.");
          }
            _context.Shelves.Add(shelf);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShelf", new { id = shelf.ShelfId }, shelf);
        }

        // DELETE: api/Shelves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShelf(int id)
        {
            if (_context.Shelves == null)
            {
                return NotFound();
            }
            var shelf = await _context.Shelves.FindAsync(id);
            if (shelf == null)
            {
                return NotFound();
            }

            _context.Shelves.Remove(shelf);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShelfExists(int id)
        {
            return (_context.Shelves?.Any(e => e.ShelfId == id)).GetValueOrDefault();
        }
    }
}
