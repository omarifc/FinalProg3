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
    public class PaisesController : ControllerBase
    {
        private readonly FutbolDbContext _context;

        public PaisesController(FutbolDbContext context)
        {
            _context = context;
        }

        // GET: api/Paises
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaisDto>>> GetPaises()
        {
            //return await _context.Paises.Include("Ligas").Include("Futbolistas").ToListAsync();
            return await _context.Paises.Include(l=>l.Ligas)
            .Include(e=>e.Equipos)
            .Include(f=>f.Futbolistas)
            .Select(p=> new PaisDto(){
                PaisId=p.PaisId,
                Nombre=p.Nombre,
                Ligas=p.Ligas.Where(x=>x.Pais.PaisId==p.PaisId).Select(o=> new LigaDto(){
                    LigaId=o.LigaId,
                    Nombre=o.Nombre,
                }).ToList(),
                Equipos=p.Equipos.Where(x=>x.Pais.PaisId==p.PaisId).Select(o=> new EquipoDto(){
                    EquipoId=o.EquipoId,
                    Nombre=o.Nombre,  
                    PartidosGanados=o.PartidosGanados,
                    PartidosEmpatados=o.PartidosEmpatados,
                    PartidosPerdidos=o.PartidosPerdidos,                
                }).ToList(),
                Futbolistas=p.Futbolistas.Where(x=>x.Pais.PaisId==p.PaisId).Select(o=> new FutbolistaDto(){
                    FutbolistaId=o.FutbolistaId,
                    Nombre=o.Nombre,
                    Goles=o.Goles,
                }).ToList(),
            })
            .ToListAsync();
        }

        // GET: api/Paises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaisDto>> GetPais(int id)
        {
            //var pais = await _context.Paises.FindAsync(id);
            
            var pais = await _context.Paises.Include(l=>l.Ligas)
            .Include(e=>e.Equipos)
            .Include(f=>f.Futbolistas)
            .Select(p=> new PaisDto(){
                PaisId=p.PaisId,
                Nombre=p.Nombre,
                Ligas=p.Ligas.Where(x=>x.Pais.PaisId==p.PaisId).Select(o=> new LigaDto(){
                    LigaId=o.LigaId,
                    Nombre=o.Nombre,
                }).ToList(),
                Equipos=p.Equipos.Where(x=>x.Pais.PaisId==p.PaisId).Select(o=> new EquipoDto(){
                    EquipoId=o.EquipoId,
                    Nombre=o.Nombre,
                    PartidosGanados=o.PartidosGanados,
                    PartidosEmpatados=o.PartidosEmpatados,
                    PartidosPerdidos=o.PartidosPerdidos,                  
                }).ToList(),
                Futbolistas=p.Futbolistas.Where(x=>x.Pais.PaisId==p.PaisId).Select(o=> new FutbolistaDto(){
                    FutbolistaId=o.FutbolistaId,
                    Nombre=o.Nombre,
                    Goles=o.Goles,
                }).ToList(),
            })
            
            .Where(x=> x.PaisId==id)
            .FirstOrDefaultAsync<PaisDto>();




            if (pais == null)
            {
                return NotFound();
            }

            return pais;
        }

        // PUT: api/Paises/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPais(int id, Pais pais)
        {
            if (id != pais.PaisId)
            {
                return BadRequest();
            }

            _context.Entry(pais).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaisExists(id))
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

        // POST: api/Paises
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pais>> PostPais(Pais pais)
        {
            _context.Paises.Add(pais);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPais", new { id = pais.PaisId }, pais);
        }

        // DELETE: api/Paises/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pais>> DeletePais(int id)
        {
            var pais = await _context.Paises.FindAsync(id);
            if (pais == null)
            {
                return NotFound();
            }

            _context.Paises.Remove(pais);
            await _context.SaveChangesAsync();

            return pais;
        }

        private bool PaisExists(int id)
        {
            return _context.Paises.Any(e => e.PaisId == id);
        }
    }
}
