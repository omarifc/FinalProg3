using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyFutbol.Models;

namespace ProyFutbol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosicionesController : ControllerBase
    {
        private readonly FutbolDbContext _context;

        public PosicionesController(FutbolDbContext context)
        {
            _context = context;
        }

        // GET: api/Posiciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Posicion>>> GetPosiciones()
        {
            return await _context.Posiciones.ToListAsync();
        }

        // GET: api/Posiciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Posicion>> GetPosicion(int id)
        {
            var posicion = await _context.Posiciones.FindAsync(id);

            if (posicion == null)
            {
                return NotFound();
            }

            return posicion;
        }

        // PUT: api/Posiciones/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPosicion(int id, Posicion posicion)
        {
            if (id != posicion.PosicionId)
            {
                return BadRequest();
            }

            _context.Entry(posicion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PosicionExists(id))
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

        // POST: api/Posiciones
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Posicion>> PostPosicion(Posicion posicion)
        {
            _context.Posiciones.Add(posicion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPosicion", new { id = posicion.PosicionId }, posicion);
        }

        // DELETE: api/Posiciones/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Posicion>> DeletePosicion(int id)
        {
            var posicion = await _context.Posiciones.FindAsync(id);
            if (posicion == null)
            {
                return NotFound();
            }

            _context.Posiciones.Remove(posicion);
            await _context.SaveChangesAsync();

            return posicion;
        }

        private bool PosicionExists(int id)
        {
            return _context.Posiciones.Any(e => e.PosicionId == id);
        }
    }
}
