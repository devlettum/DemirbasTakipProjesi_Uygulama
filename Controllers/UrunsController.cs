using DemirbasTakipProjesi_Uygulama.DetailResponses;
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
    public class UrunsController : ControllerBase
    {
        private readonly DemirbasTakipContext _context;

        public UrunsController(DemirbasTakipContext context)
        {
            _context = context;
        }

        // GET: api/urunler
        [HttpGet]
        public IActionResult GetUrunler()
        {
            System.Threading.Thread.Sleep(1000);


            var urunler = _context.Urunler.Include(a => a.AlanKisiNavigation).ToList().Select(y => new UrunResponse()
            {
                Id = y.Id,
                BirimFiyati = y.BirimFiyati,
                SeriNo = y.SeriNo,
                UrunIsim = y.UrunIsim,
                Insan = new InsanResponse() { Id = y.AlanKisi, Ad = y.AlanKisiNavigation.Ad, Soyad = y.AlanKisiNavigation.Soyad }
            });

            return Ok(urunler);
        }
        [HttpGet("{id}")]
        public IActionResult GetUrun(int id)
        {
            var urun = _context.Urunler.Include(a => a.AlanKisiNavigation).FirstOrDefault(z => z.Id == id);

            var urunResponse = new UrunResponse()
            {
                Id = urun.Id,
                BirimFiyati = urun.BirimFiyati,
                SeriNo = urun.SeriNo,
                UrunIsim = urun.UrunIsim,
                Insan = new InsanResponse() { Id = urun.AlanKisi, Ad = urun.AlanKisiNavigation.Ad, Soyad = urun.AlanKisiNavigation.Soyad }
            };

            return Ok(urunResponse);
        }


        // PUT: api/urunler/5
        [HttpPut("{id}")]
        public  IActionResult UrunGuncelle(Urunler urun)
        {
            //if (id != urun.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(urun).State = EntityState.Modified;

            //try
            //{
            //    _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UrunExist(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}


            //var guncellenecekUrun = _context.Urunler.Where(x => x.Id == urun.Id).FirstOrDefault();
            //if (guncellenecekUrun!=null)
            //{
            //    guncellenecekUrun.SeriNo = urun.SeriNo;
            //    guncellenecekUrun.BirimFiyati = urun.BirimFiyati;
            //    guncellenecekUrun.AlanKisi = urun.AlanKisi;
            //    _context.SaveChanges();

            //}


            //Çalışan Hali

            var guncellenecekUrun = _context.Entry(urun);
            guncellenecekUrun.State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();


            //var guncellenecekUrun = _context.Urunler.Find(id);

            //guncellenecekUrun.UrunIsim = urun.UrunIsim;
            //guncellenecekUrun.BirimFiyati = urun.BirimFiyati;
            //guncellenecekUrun.SeriNo = urun.SeriNo;
            ////guncellenecekUrun.AlanKisi = urun.AlanKisi;
            ////guncellenecekUrun.AlanKisiNavigation = urun.AlanKisiNavigation;
            //_context.SaveChanges();

            //return Ok();
        }

        // POST: api/urunler

        [HttpPost]
        public IActionResult PostUrun(Urunler urun)
        {
            _context.Urunler.Add(urun);
            _context.SaveChanges();

            return Ok(urun);
        }

        // DELETE: api/urunler/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Urunler>> DeleteUrun(int id)
        {
            var urun = await _context.Urunler.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }

            _context.Urunler.Remove(urun);
            await _context.SaveChangesAsync();

            return urun;
        }

        private bool UrunExist(int id)
        {
            return _context.Urunler.Any(e => e.Id == id);
        }
    }
}