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
    public class EquiposController : ControllerBase
    {
        private readonly FutbolDbContext _context;

        public EquiposController(FutbolDbContext context)
        {
            _context = context;
        }

        // GET: api/Equipos
        [HttpGet]        
        public async Task<ActionResult<IEnumerable<EquipoDto>>> GetEquipos()
        {
            //ERROR QUE QUEDÓ PENDIENTE
            return await _context.Equipos.Include(f=>f.Futbolistas).Include(j=>j.Pais).Include(l=>l.Liga)
            .Select(e=> new EquipoDto()
            {
                EquipoId=e.EquipoId,
                Nombre=e.Nombre,
                Liga=e.Liga.Nombre,     
                Pais=e.Pais.Nombre,          
                PartidosGanados=e.PartidosGanados,
                PartidosEmpatados=e.PartidosEmpatados,
                PartidosPerdidos=e.PartidosPerdidos,
                Futbolistas=e.Futbolistas.Where(x=> x.Equipo.EquipoId==e.EquipoId).Select(o=> new FutbolistaDto(){
                    FutbolistaId=o.FutbolistaId,
                    Nombre=o.Nombre,
                    Edad=o.Edad,
                    Descripcion=o.Descripcion,
                    Goles=o.Goles,
                    Pais=o.Pais.Nombre,
                    PosDto=o.FutbolistaPosiciones.Where(x=> x.FutbolistaId==o.FutbolistaId).Select(p=> new PosicionDto()
                    { Id=p.PosicionId, Nombre=p.Posicion.Nombre}).ToList(),              
                }).ToList<FutbolistaDto>()
            }).ToListAsync(); 
        }




        // GET: api/Equipos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipoDto>> GetEquipo(int id)
        {
            var equipo = await _context.Equipos.Include(p=>p.Pais).Include(l=>l.Liga)
            .Include(x=>x.Futbolistas)
            .Select(e=> new EquipoDto(){
                EquipoId=e.EquipoId,
                Nombre=e.Nombre,
                Liga=e.Liga.Nombre,     
                Pais=e.Pais.Nombre,   
                PartidosGanados=e.PartidosGanados,
                PartidosEmpatados=e.PartidosEmpatados,
                PartidosPerdidos=e.PartidosPerdidos,
                Futbolistas=e.Futbolistas.Where(x=> x.Equipo.EquipoId==e.EquipoId).Select(o=> new FutbolistaDto(){
                    FutbolistaId=o.FutbolistaId,
                    Nombre=o.Nombre,
                    Edad=o.Edad,
                    Goles=o.Goles,
                    Pais=o.Pais.Nombre,
                    PosDto=o.FutbolistaPosiciones.Where(x=> x.FutbolistaId==o.FutbolistaId).Select(p=> new PosicionDto()
                    { Id=p.PosicionId, Nombre=p.Posicion.Nombre}).ToList(),
                }).ToList<FutbolistaDto>()
            
            })
            .Where(x=> x.EquipoId==id)
            .FirstOrDefaultAsync<EquipoDto>();

            //var equipo = await _context.Equipos.Include(e => e.Futbolistas)
            //.Where(e=> e.EquipoId==id)
            //.FirstOrDefaultAsync();

            if (equipo == null)
            {
                return NotFound();
            }
            return equipo;
        }


        [HttpGet("PromedioEdad/{Id}")]
        public IActionResult GetPromedioEdad(int Id)
        {
            var promedio=0;
            var futbolistas =  _context.Futbolistas.Where(e=> e.Equipo.EquipoId==Id).ToList();
               if (futbolistas == null)
            {
                return NotFound();
            }
         
            var suma=futbolistas.Sum(x=>x.Edad);
            promedio=suma/futbolistas.Count();

            return Ok(promedio);
        }

        //Prueba de NonAction
        [NonAction]
        [HttpGet]
        public IActionResult ActualizarTabla(){
            //Prueba
            return null;
        }

        
        // Muestra un futbolista ingresando el Id
        [HttpGet("{id}/Futbolistas")]
        public async Task<ActionResult<FutbolistaDto>> GetFutbolista(int id)
        {
            
            var futbolista = await _context.Futbolistas.Include("Pais")
            .Select(z=>new FutbolistaDto(){
                    FutbolistaId=z.FutbolistaId,
                    Nombre=z.Nombre,
                    Edad=z.Edad,
                    Pais=z.Pais.Nombre,
                    Descripcion=z.Descripcion,
                    Equipo=z.Equipo.Nombre,
                    Goles=z.Goles,
                    PosDto=z.FutbolistaPosiciones.Where(x=> x.FutbolistaId==z.FutbolistaId).Select(o=> new PosicionDto()
                      { Id=o.PosicionId, Nombre=o.Posicion.Nombre}).ToList(),
                    Contrato=z.Contrato
            } )
            .Where(j=>j.FutbolistaId==id).FirstOrDefaultAsync<FutbolistaDto>();
           
            if (futbolista == null)
            {
                return NotFound();
            }


            return futbolista;
        }

        // PUT: api/Equipos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipo(int id, Equipo equipo)
        {
            if (id != equipo.EquipoId)
            {
                return BadRequest();
            }

            _context.Entry(equipo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

            //ActualizarTabla();
        }

        // POST: api/Equipos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<EquipoDto>> PostEquipo(Equipo equipo)
        {
            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipo", new { id = equipo.EquipoId }, equipo);
        }

        // DELETE: api/Equipos/5
        [NonAction]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Equipo>> DeleteEquipo(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();

            return equipo;
            
            //ActualizarTabla();
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.EquipoId == id);
        }

    }
}
