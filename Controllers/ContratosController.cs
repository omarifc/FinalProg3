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
    public class ContratosController : ControllerBase
    {
        private readonly FutbolDbContext _context;

        public ContratosController(FutbolDbContext context)
        {
            _context = context;
        }

        // GET: api/Contratos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoDto>>> GetContratos()
        {
            //return await _context.Contratos.ToListAsync();     
             return await _context.Contratos.Include(f=>f.Futbolista)
            .Select(c=> new ContratoDto(){
                ContratoId=c.ContratoId,
                Descripcion=c.Descripcion,
                Sueldo=c.Sueldo,
                Inicio=c.Inicio,
                Duracion=c.Duracion,
                FutbolistaId=c.FutbolistaId,
                Futbolista=c.Futbolista.Nombre,
            }
            )
            .ToListAsync();      
        }

        //[HttpGet("{sueldo}")]
        //public async Task<ActionResult<ContratoDto>> GetContratoBySueldo(double sueldo)
        //{
        //    if ()
        //    {
        //        var contrato = await _context.Contratos.Include("Futbolista")
        //        .Select(c=>new ContratoDto(){
        //        ContratoId=c.ContratoId,
        //       Descripcion=c.Descripcion,
        //        Sueldo=c.Sueldo,
        //        Inicio=c.Inicio,
        //        Duracion=c.Duracion,
        //        FutbolistaId=c.FutbolistaId,
        //        Futbolista=c.Futbolista.Nombre,
        //    })
        //    .FirstOrDefaultAsync<ContratoDto>();

        //    if (contrato == null)
        //    {
        //        return NotFound();
        //    }

        //    return contrato;
        //    } 

        //    else    

        //    return NotFound();
    
        //}

        [HttpGet("MasAlto")]
        public async Task<ActionResult<ContratoDto>> GetContratoMasAlto()
        {
            return await _context.Contratos.Include(c => c.Futbolista).OrderByDescending(c => c.Sueldo)
            .Select(z=> new ContratoDto()
                {
                    ContratoId=z.ContratoId,
                    Descripcion=z.Descripcion,
                    Sueldo=z.Sueldo,
                    Inicio=z.Inicio,
                    Duracion=z.Duracion,
                    Futbolista=z.Futbolista.Nombre
                }).FirstOrDefaultAsync();
            //return await _context.Contratos.ToListAsync();
        }

        
        [HttpGet("Superiores/{Monto}")]
        public async Task<ActionResult<IEnumerable<ContratoDto>>> GetContratosSuperior(double Monto)
        {
            return await _context.Contratos.Include(c => c.Futbolista)
            .Where(c => c.Sueldo>= Monto)
            .Select(z=> new ContratoDto()
                {
                    ContratoId=z.ContratoId,
                    Descripcion=z.Descripcion,
                    Sueldo=z.Sueldo,
                    Inicio=z.Inicio,
                    Duracion=z.Duracion,
                    Futbolista=z.Futbolista.Nombre
                }).ToListAsync(); 
        }
        
        // GET: api/Contratos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContratoDto>> GetContrato(int id)
        {
            var contrato = await _context.Contratos.Include("Futbolista")
            .Select(c=>new ContratoDto(){
                ContratoId=c.ContratoId,
                Descripcion=c.Descripcion,
                Sueldo=c.Sueldo,
                Inicio=c.Inicio,
                Duracion=c.Duracion,
                FutbolistaId=c.FutbolistaId,
                Futbolista=c.Futbolista.Nombre,
            })
            .Where(j=>j.ContratoId==id).FirstOrDefaultAsync<ContratoDto>();

            if (contrato == null)
            {
                return NotFound();
            }

            return contrato;
        }

        // PERMITE MODIFICAR CON LOS ID MENORES A 1000
        // PUT: api/Contratos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContrato(int id, Contrato contrato)
        {
            if (id < 1000)
            { 

            if (id != contrato.ContratoId)
            {
                return BadRequest();
            }

            _context.Entry(contrato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContratoExists(id))
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
            else   
                return NotFound();
        }

        // POST: api/Contratos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Contrato>> PostContrato(Contrato contrato)
        {
            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContrato", new { id = contrato.ContratoId }, contrato);
        }

        // DELETE: api/Contratos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contrato>> DeleteContrato(int id)
        {
            var contrato = await _context.Contratos.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }

            _context.Contratos.Remove(contrato);
            await _context.SaveChangesAsync();

            return contrato;
        }

        private bool ContratoExists(int id)
        {
            return _context.Contratos.Any(e => e.ContratoId == id);
        }
    }
}
