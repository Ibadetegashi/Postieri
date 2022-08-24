﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Postieri.Data;
using Postieri.Models;

namespace Postieri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly DataContext _context;

        public WarehousesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Warehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouse()
        {
          if (_context.Warehouse == null)
          {
              return NotFound();
          }
            return await _context.Warehouse.ToListAsync();
        }

        // GET: api/Warehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(int id)
        {
          if (_context.Warehouse == null)
          {
              return NotFound();
          }
            var warehouse = await _context.Warehouse.FindAsync(id);

            if (warehouse == null)
            {
                return NotFound();
            }

            return warehouse;
        }

        // PUT: api/Warehouses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(int id, Warehouse warehouse)
        {
            if (id != warehouse.WarehouseId)
            {
                return BadRequest();
            }

            _context.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseExists(id))
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

        // POST: api/Warehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(Warehouse warehouse)
        {
          if (_context.Warehouse == null)
          {
              return Problem("Entity set 'DataContext.Warehouse'  is null.");
          }
            _context.Warehouse.Add(warehouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWarehouse", new { id = warehouse.WarehouseId }, warehouse);
        }

        // DELETE: api/Warehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            if (_context.Warehouse == null)
            {
                return NotFound();
            }
            var warehouse = await _context.Warehouse.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            _context.Warehouse.Remove(warehouse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WarehouseExists(int id)
        {
            return (_context.Warehouse?.Any(e => e.WarehouseId == id)).GetValueOrDefault();
        }
    }
}
