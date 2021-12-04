using DemirbasTakipProjesi_Uygulama.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemirbasTakipProjesi_Uygulama.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsansController : ControllerBase
    {
        private readonly DemirbasTakipContext _context;

        public InsansController(DemirbasTakipContext context)
        {
            _context = context;
        }

        // GET: api/insanlar
        [HttpGet]  //Attributes 
        public async Task<ActionResult<IEnumerable<Insanlar>>> GetInsanlar()
        {
            System.Threading.Thread.Sleep(1000);

            return await _context.Insanlar.ToListAsync();
        }

        // GET: api/insanlar/5
        [HttpGet("{id}")] //Dinamik
        public async Task<ActionResult<Insanlar>> GetInsan(int id)
        {
            var insan = await _context.Insanlar.FindAsync(id);

            if (insan == null)
            {
                return NotFound();
            }

            return insan;
        }

        //CRUD --> Create , Read , Update , Delete 

        // PUT: api/insanlar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsan(int id,Insanlar insan)
        {
            insan =  await _context.Insanlar.FindAsync(id);
            if (id != insan.Id)
            {
                return BadRequest();
            }
            _context.Entry(insan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsanExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
           
            return Ok();
        }

        // POST: api/insanlar

        [HttpPost]
        public async Task<ActionResult<Insanlar>> PostInsan(Insanlar insan)
        {
            _context.Insanlar.Add(insan);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsan", new { id = insan.Id }, insan);
        }

        // DELETE: api/insanlar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsan(int id)
        {
            var insan = await _context.Insanlar.FindAsync(id);
            if (insan == null)
            {
                return NotFound();
            }

            _context.Insanlar.Remove(insan);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool InsanExist(int id)
        {
            return _context.Insanlar.Any(e => e.Id == id);
        }
    }
}