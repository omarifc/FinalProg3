using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyFutbol.Models;
using Microsoft.AspNetCore.Authorization;

namespace ProyFutbol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FutbolistasController : ControllerBase
    {
        private readonly FutbolDbContext _context;

        public FutbolistasController(FutbolDbContext context)
        {
            _context = context;
        }

        // GET: api/Futbolistas
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<FutbolistaDto>>> GetFutbolistas()
        {
            //var posiciones=
            return await _context.Futbolistas.Include(x => x.Pais)
            .Include(f => f.Equipo)
            .Include(c => c.Contrato)
            .Select(z => new FutbolistaDto()
            {
                FutbolistaId = z.FutbolistaId,
                Nombre = z.Nombre,
                Edad = z.Edad,
                Pais = z.Pais.Nombre,
                Descripcion = z.Descripcion,
                Equipo = z.Equipo.Nombre,
                Goles = z.Goles,
                //Posiciones=_context.FutbolistaPosiciones.Where(x=> x.FutbolistaId==z.FutbolistaId).Select(x=> x.Posicion).ToList()
                //Posiciones=z.FutbolistaPosiciones.Where(x=> x.FutbolistaId==z.FutbolistaId).Select(o=> o.Posicion).ToList()
                Posiciones = z.FutbolistaPosiciones.Where(x => x.FutbolistaId == z.FutbolistaId).Select(o => new PosicionDto()
                { Id = o.PosicionId, Nombre = o.Posicion.Nombre }).ToList(),
                Contrato = z.Contrato
            }
            )
            .ToListAsync();
        }


        // GET: api/Futbolistas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FutbolistaDto>> GetFutbolista(int id)
        {
            var futbolista = await _context.Futbolistas.Include("Pais")
            .Select(z => new FutbolistaDto()
            {
                FutbolistaId = z.FutbolistaId,
                Nombre = z.Nombre,
                Edad = z.Edad,
                Pais = z.Pais.Nombre,
                Descripcion = z.Descripcion,
                Equipo = z.Equipo.Nombre,
                Goles = z.Goles,
                PosDto = z.FutbolistaPosiciones.Where(x => x.FutbolistaId == z.FutbolistaId).Select(o => new PosicionDto()
                { Id = o.PosicionId, Nombre = o.Posicion.Nombre }).ToList(),
                Contrato = z.Contrato
            })
            .Where(j => j.FutbolistaId == id).FirstOrDefaultAsync<FutbolistaDto>();

            if (futbolista == null)
            {
                return NotFound("No se encontró el futbolista con ese id");
            }

            return futbolista;
        }

        // Muestra futbolista por país --Ok--
        [HttpGet("PorPais/{id}")]
        public async Task<ActionResult<IEnumerable<FutbolistaDto>>> GetFutbolistaPorPais(int id)
        {
            var futbolista = await _context.Futbolistas.Include("Pais")
            .Where(c => c.Pais.PaisId == id)
            .Select(z => new FutbolistaDto()
            {
                FutbolistaId = z.FutbolistaId,
                Nombre = z.Nombre,
                Edad = z.Edad,
                Pais = z.Pais.Nombre,
                Descripcion = z.Descripcion,
                Equipo = z.Equipo.Nombre,
                Goles = z.Goles,
                PosDto = z.FutbolistaPosiciones.Where(x => x.FutbolistaId == z.FutbolistaId).Select(o => new PosicionDto()
                { Id = o.PosicionId, Nombre = o.Posicion.Nombre }).ToList(),
                Contrato = z.Contrato
            })
            .ToListAsync();

            if (futbolista == null)
            {
                return NotFound();
            }

            return futbolista;
        }


        // Devuelve un futbolista buscado por nombre
        [HttpGet("PorNombre/{nombre}")]
        public async Task<ActionResult<FutbolistaDto>> GetFutbolistaPorNombre(string nombre)
        {
            var futbolista = await _context.Futbolistas.Include("Pais")
            .Where(c => c.Nombre == nombre)
            .Select(z => new FutbolistaDto()
            {
                FutbolistaId = z.FutbolistaId,
                Nombre = z.Nombre,
                Edad = z.Edad,
                Pais = z.Pais.Nombre,
                Descripcion = z.Descripcion,
                Equipo = z.Equipo.Nombre,
                Goles = z.Goles,
                PosDto = z.FutbolistaPosiciones.Where(x => x.FutbolistaId == z.FutbolistaId).Select(o => new PosicionDto()
                { Id = o.PosicionId, Nombre = o.Posicion.Nombre }).ToList(),
                Contrato = z.Contrato
            })
            .FirstOrDefaultAsync<FutbolistaDto>();

            if (futbolista == null)
            {
                return NotFound();
            }

            return futbolista;
        }


        // Devuelve al Goleador
        [HttpGet("Goleador")]
        public async Task<ActionResult<FutbolistaDto>> FutbolistaGoleador()
        {
            return await _context.Futbolistas.Include("Pais").OrderByDescending(c => c.Goles)
            .Select(z => new FutbolistaDto()
            {
                FutbolistaId = z.FutbolistaId,
                Nombre = z.Nombre,
                Edad = z.Edad,
                Pais = z.Pais.Nombre,
                Descripcion = z.Descripcion,
                Equipo = z.Equipo.Nombre,
                Goles = z.Goles,
                PosDto = z.FutbolistaPosiciones.Where(x => x.FutbolistaId == z.FutbolistaId).Select(o => new PosicionDto()
                { Id = o.PosicionId, Nombre = o.Posicion.Nombre }).ToList(),
                Contrato = z.Contrato
            })
            .FirstOrDefaultAsync<FutbolistaDto>();
        }

        [HttpGet("busqueda/{nombre}/equipo/{edad}")]
        public async Task<ActionResult<FutbolistaDto>> busqueda(string nombre, string equipo)
        {
            var futbolista = await _context.Futbolistas.Include("Pais")
            .Where(c => c.Nombre == nombre.ToUpper() || c.Equipo.Nombre == equipo)
            .Select(z => new FutbolistaDto()
            {
                FutbolistaId = z.FutbolistaId,
                Nombre = z.Nombre,
                Edad = z.Edad,
                Pais = z.Pais.Nombre,
                Descripcion = z.Descripcion,
                Equipo = z.Equipo.Nombre,
                Goles = z.Goles,
                PosDto = z.FutbolistaPosiciones.Where(x => x.FutbolistaId == z.FutbolistaId).Select(o => new PosicionDto()
                { Id = o.PosicionId, Nombre = o.Posicion.Nombre }).ToList(),
                Contrato = z.Contrato
            })
            .FirstOrDefaultAsync<FutbolistaDto>();

            if (futbolista == null)
            {
                return NotFound("No se encontró futbolista con los parametros especificados");
            }

            return futbolista;
        }

        // POST: api/Futbolistas
        [HttpPost]
        public async Task<ActionResult<FutbolistaDto>> PostFutbolista(FutbolistaDto futbolistaDto)
        {
            var futbolista = new Futbolista
            {
                FutbolistaId = futbolistaDto.FutbolistaId,
                Nombre = futbolistaDto.Nombre,
                Edad = futbolistaDto.Edad,
                Descripcion = futbolistaDto.Descripcion,
                Goles = futbolistaDto.Goles,
            };
            _context.Futbolistas.Add(futbolista);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFutbolista", new { id = futbolista.FutbolistaId }, futbolista);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFutbolista(int id, FutbolistaDto futbolistaDto)
        {
            if (id != futbolistaDto.FutbolistaId)
            {
                return BadRequest();
            }

            var futbolista = await _context.Futbolistas.FindAsync(id);
            if (futbolista == null)
            {
                return NotFound();
            }

            futbolista.Nombre = futbolistaDto.Nombre;
            futbolista.Edad = futbolistaDto.Edad;
            futbolista.Descripcion = futbolistaDto.Descripcion;
            futbolista.Goles = futbolistaDto.Goles;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!FutbolistaExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Futbolistas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Futbolista>> DeleteFutbolista(int id)
        {
            var futbolista = await _context.Futbolistas.FindAsync(id);
            if (futbolista == null)
            {
                return NotFound();
            }

            _context.Futbolistas.Remove(futbolista);
            await _context.SaveChangesAsync();

            return Ok("Futbolista eliminado");
        }

        private bool FutbolistaExists(int id)
        {
            return _context.Futbolistas.Any(e => e.FutbolistaId == id);
        }

    }
}
