using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recepti.WebApi.Database;
using Recepti.WebApi.Model;

namespace Recepti.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategorijeController : ControllerBase
    {
        private readonly ReceptiContext _context;

        public KategorijeController(ReceptiContext context)
        {
            _context = context;
        }

        // GET: api/Kategorije
        [HttpGet("GetAll/")]
        public ActionResult<KategorijeVM> GetAll()
        {
            var model = new KategorijeVM
            {
                rows = _context.Kategorije                    
                    .Select(s => new KategorijeVM.Row
                    {
                        
                        kategorijaId=s.KategorijaId,
                        naziv=s.Naziv
                    }).ToList()
            };
            return model;
        }

        // GET: api/Kategorije/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKategorije([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kategorije = await _context.Kategorije.FindAsync(id);

            if (kategorije == null)
            {
                return NotFound();
            }

            return Ok(kategorije);
        }

        // PUT: api/Kategorije/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKategorije([FromRoute] int id, [FromBody] Kategorije kategorije)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kategorije.KategorijaId)
            {
                return BadRequest();
            }

            _context.Entry(kategorije).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategorijeExists(id))
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

        // POST: api/Kategorije
        [HttpPost]
        public async Task<IActionResult> PostKategorije([FromBody] Kategorije kategorije)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Kategorije.Add(kategorije);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKategorije", new { id = kategorije.KategorijaId }, kategorije);
        }

        // DELETE: api/Kategorije/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKategorije([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var kategorije = await _context.Kategorije.FindAsync(id);
            if (kategorije == null)
            {
                return NotFound();
            }

            _context.Kategorije.Remove(kategorije);
            await _context.SaveChangesAsync();

            return Ok(kategorije);
        }

        private bool KategorijeExists(int id)
        {
            return _context.Kategorije.Any(e => e.KategorijaId == id);
        }
    }
}