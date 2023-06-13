﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDemo2.Models;

namespace APIDemo2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleFacturasController : ControllerBase
    {
        private readonly DemoContext _context;

        public DetalleFacturasController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/DetalleFacturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFactura>>> GetDetalleFacturas()
        {
          if (_context.DetalleFacturas == null)
          {
              return NotFound();
          }
            return await _context.DetalleFacturas.ToListAsync();
        }

        // GET: api/DetalleFacturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleFactura>> GetDetalleFactura(int id)
        {
          if (_context.DetalleFacturas == null)
          {
              return NotFound();
          }
            var detalleFactura = await _context.DetalleFacturas.FindAsync(id);

            if (detalleFactura == null)
            {
                return NotFound();
            }

            return detalleFactura;
        }

        // PUT: api/DetalleFacturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleFactura(int id, DetalleFactura detalleFactura)
        {
            if (id != detalleFactura.DetalleFacturaID)
            {
                return BadRequest();
            }

            _context.Entry(detalleFactura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleFacturaExists(id))
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

        // POST: api/DetalleFacturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleFactura>> PostDetalleFactura(DetalleFactura detalleFactura)
        {
          if (_context.DetalleFacturas == null)
          {
              return Problem("Entity set 'DemoContext.DetalleFacturas'  is null.");
          }
            _context.DetalleFacturas.Add(detalleFactura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleFactura", new { id = detalleFactura.DetalleFacturaID }, detalleFactura);
        }

        // DELETE: api/DetalleFacturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleFactura(int id)
        {
            if (_context.DetalleFacturas == null)
            {
                return NotFound();
            }
            var detalleFactura = await _context.DetalleFacturas.FindAsync(id);
            if (detalleFactura == null)
            {
                return NotFound();
            }

            _context.DetalleFacturas.Remove(detalleFactura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleFacturaExists(int id)
        {
            return (_context.DetalleFacturas?.Any(e => e.DetalleFacturaID == id)).GetValueOrDefault();
        }
    }
}
