using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIDemo2.Models;
using System.Runtime.CompilerServices;

namespace APIDemo2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly DemoContext _context;

        public FacturasController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/Facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
          if (_context.Facturas == null)
          {
              return NotFound();
          }
            //return await _context.Facturas.Include(x => x.DetalleFacturas.ToList().ForEach(y=> _context.Products.Find(y.ProductID)).ToListAsync();
            return await _context.Facturas.Where(f => f.Estado != false).Include(x => x.DetalleFacturas).ThenInclude(detalle => detalle.Product).ThenInclude(category => category.Category).ToListAsync();
        }

        // GET: api/Facturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(int id)
        {
          if (_context.Facturas == null)
          {
              return NotFound();
          }
            var factura = await _context.Facturas.FindAsync(id);

            if (factura == null)
            {
                return NotFound();
            }

            return factura;
        }

        // PUT: api/Facturas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFactura(int id, Factura factura)
        {
            if (id != factura.FacturaID)
            {
                return BadRequest();
            }

            _context.Entry(factura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
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

        // POST: api/Facturas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Factura>> PostFactura(Factura factura)
        {
          if (_context.Facturas == null)
          {
              return Problem("Entity set 'DemoContext.Facturas'  is null.");
          }
            factura.Estado = true;
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFactura", new { id = factura.FacturaID }, factura);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFactura(int id)
        {
            if (_context.Facturas == null)
            {
                return NotFound();
            }
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Facturas/5
        [HttpDelete("DeleteFacturaLogica/{id}")]
        public async Task<IActionResult> DeleteFacturaLogica(int id)
        {
            if (_context.Facturas == null)
            {
                return NotFound();
            }
            var factura = await _context.Facturas.FindAsync(id);
            factura.Estado = false;
            if (factura == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FacturaExists(int id)
        {
            return (_context.Facturas?.Any(e => e.FacturaID == id)).GetValueOrDefault();
        }
    }
}
