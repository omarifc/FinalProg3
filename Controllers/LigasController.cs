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
    public class LigasController : ControllerBase
    {
        private readonly FutbolDbContext _context;

        public LigasController(FutbolDbContext context)
        {
            _context = context;
        }

        // GET: api/Ligas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LigaDto>>> GetLigas()
        {
            return await _context.Ligas.Include(p=>p.Pais).Include(x=>x.Equipos)
            .Select(l=> new LigaDto(){
                LigaId=l.LigaId,
                Nombre=l.Nombre,
                Pais=l.Pais.Nombre,
                Equipos=l.Equipos.Where(x=> x.Liga.LigaId==l.LigaId).Select(o=> new EquipoDto(){
                    Nombre=o.Nombre,
                    EquipoId=o.EquipoId,
                    PartidosGanados=o.PartidosGanados,
                    PartidosEmpatados=o.PartidosEmpatados,
                    PartidosPerdidos=o.PartidosPerdidos,
                }).ToList<EquipoDto>()
            })
            .ToListAsync();
        }

        // GET: api/Ligas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LigaDto>> GetLiga(int id)
        {
            var liga = await _context.Ligas.Include(p=>p.Pais)
            .Include(x=>x.Equipos)
            .Select(l=> new LigaDto(){
                LigaId=l.LigaId,
                Nombre=l.Nombre,
                Pais=l.Pais.Nombre,
                Equipos=l.Equipos.Where(x=> x.Liga.LigaId==l.LigaId).Select(o=> new EquipoDto(){
                    EquipoId=o.EquipoId,
                    Nombre=o.Nombre,
                    PartidosGanados=o.PartidosGanados,
                    PartidosEmpatados=o.PartidosEmpatados,
                    PartidosPerdidos=o.PartidosPerdidos,
                }).ToList<EquipoDto>()
            
            })
            .Where(x=> x.LigaId==id)
            .FirstOrDefaultAsync<LigaDto>();

            if (liga == null)
            {
                return NotFound();
            }

            return liga;
        }

        // PUT: api/Ligas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLiga(int id, Liga liga)
        {
            if (id != liga.LigaId)
            {
                return BadRequest();
            }

            _context.Entry(liga).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LigaExists(id))
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

        // POST: api/Ligas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Liga>> PostLiga(Liga liga)
        {
            _context.Ligas.Add(liga);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLiga", new { id = liga.LigaId }, liga);
        }

        // DELETE: api/Ligas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Liga>> DeleteLiga(int id)
        {
            var liga = await _context.Ligas.FindAsync(id);
            if (liga == null)
            {
                return NotFound();
            }

            _context.Ligas.Remove(liga);
            await _context.SaveChangesAsync();

            return liga;
        }

        private bool LigaExists(int id)
        {
            return _context.Ligas.Any(e => e.LigaId == id);
        }
    }
}
