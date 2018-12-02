using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputacionMovilAPI.Models;
using Microsoft.AspNetCore.Internal;

namespace ComputacionMovilAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly ComputacionMovilDbContext _context;

        public EventosController(ComputacionMovilDbContext context)
        {
            _context = context;
        }

        // GET: api/Eventos
        [HttpGet]
        public IEnumerable<EventoWRK> GetEvento()
        {
            return _context.EventoWRK;
        }

        // GET: api/Eventos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventoWRK = await _context.EventoWRK.FindAsync(id);

            if (eventoWRK == null)
            {
                return NotFound();
            }
            else
            {
                eventoWRK.Hito = await _context.HitoMSTR.FindAsync(eventoWRK.HitoID);
                eventoWRK.Infante = await _context.InfanteMSTR.FindAsync(eventoWRK.InfanteID);
            }

            return Ok(eventoWRK);
        }

        // PUT: api/Eventos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvento([FromRoute] int id, [FromBody] EventoWRK eventoWRK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventoWRK.EventoID)
            {
                return BadRequest();
            }

            _context.Entry(eventoWRK).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoExists(id))
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

        // POST: api/Eventos
        [HttpPost]
        public async Task<IActionResult> PostEvento([FromBody] EventoWRK eventoWRK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            eventoWRK.Fecha = DateTime.Now;

            _context.EventoWRK.Add(eventoWRK);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventoExists(eventoWRK.EventoID))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            dynamic returnValue = new ExpandoObject();
            dynamic returnValueHito = new ExpandoObject();
            dynamic returnValueInfante = new ExpandoObject();

            returnValue.EventoID = eventoWRK.EventoID;
            var hito = await _context.HitoMSTR.FindAsync(eventoWRK.HitoID);
            returnValueHito.HitoID = hito.HitoID;
            returnValueHito.HitoDescripcion = hito.HitoDescripcion;

            var infante = await _context.InfanteMSTR.FindAsync(eventoWRK.InfanteID);
            returnValueInfante.InfanteID = infante.InfanteID;
            returnValueInfante.Nombre = infante.InfanteNombre;

            var infanteHitos = await _context.InfanteHitoXREF.SingleAsync(x => x.InfanteID == eventoWRK.InfanteID && x.HitoID == eventoWRK.HitoID);
            returnValueInfante.MaxEventos = infanteHitos.MaxEventos;

            var eventosTotal = await _context.EventoWRK.CountAsync(x => x.InfanteID == eventoWRK.InfanteID && x.HitoID == eventoWRK.HitoID);
            returnValueInfante.EventosTotal = eventosTotal;

            returnValue.Hito = returnValueHito;
            returnValue.Infante = returnValueInfante;

            //return CreatedAtAction("GetEvento", new { id = eventoWRK.EventoID }, eventoWRK);
            return Ok(returnValue);
        }

        // DELETE: api/Eventos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventoWRK = await _context.EventoWRK.FindAsync(id);
            if (eventoWRK == null)
            {
                return NotFound();
            }

            _context.EventoWRK.Remove(eventoWRK);
            await _context.SaveChangesAsync();

            return Ok(eventoWRK);
        }

        private bool EventoExists(int id)
        {
            return _context.EventoWRK.Any(e => e.EventoID == id);
        }
    }
}