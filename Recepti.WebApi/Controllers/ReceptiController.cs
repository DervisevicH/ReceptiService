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
    public class ReceptiController : ControllerBase
    {
        private readonly ReceptiContext _db;

        public ReceptiController(ReceptiContext context)
        {
            _db = context;
        }
        [HttpGet("Index")]
        public ActionResult<ReceptiVM> Index()
        {
            var model = new ReceptiVM
            {
                rows = _db.Recepti
                   .OrderByDescending(s => s.ReceptId)
                   .Select(s => new ReceptiVM.Row
                   {
                       naziv=s.Naziv,opis=s.Opis,sastojci=s.Sastojci,vrijemeKuhanja=s.VrijemeKuhanja,receptId=s.ReceptId,slika=s.Slika,level=s.Level

                   }).ToList()
            };


            return model;
        }
        [HttpGet("GetReceptByNaziv/{naziv}")]
        public ActionResult<ReceptiVM> GetReceptByNaziv(string naziv)
        {
            var model = new ReceptiVM
            {
                rows = _db.Recepti
                .Where(w => (w.Naziv).ToLower().StartsWith(naziv.ToLower()))
                .Select(s => new ReceptiVM.Row
                {
                    naziv = s.Naziv,
                    opis = s.Opis,
                    sastojci = s.Sastojci,
                    vrijemeKuhanja = s.VrijemeKuhanja,
                    receptId = s.ReceptId,
                    slika=s.Slika,
                    level=s.Level
                }).ToList()
            };
            return model;

        }
        [HttpGet("GetById/{id}")]
        public ActionResult<ReceptiVM.Row> GetById([FromRoute]int id)
        {
            var result = _db.Recepti
                   .Where(w => w.ReceptId == id)
                   .Select(s => new ReceptiVM.Row
                   {
                       naziv = s.Naziv,
                       opis = s.Opis,
                       sastojci = s.Sastojci,
                       vrijemeKuhanja = s.VrijemeKuhanja,
                       receptId = s.ReceptId,
                       slika = s.Slika,
                       level=s.Level
                       

                       

                   })
                    .Single();

            return result;
        }
        [HttpGet("GetReceptiByKorisnik/{korisnikId}")]
        public ActionResult<List<Recepti.WebApi.Database.Recepti>> GetReceptiByKorisnik([FromRoute]int korisnikId)
        {
            List<Recepti.WebApi.Database.Recepti> lista = _db.Recepti.Where(x => x.KorisnikId == korisnikId).ToList();
            if (lista == null)
            {
                return NotFound();
            }
            else
                return lista;
        }
        
        [HttpGet("GetReceptiByKategorija/{kategorijaId}")]
        public ActionResult<ReceptiVM> GetReceptiByKategorija([FromRoute]int kategorijaId)
        {
            var model = new ReceptiVM
            {
                rows = _db.Recepti.Where(x=>x.KategorijaId==kategorijaId)
                   .OrderByDescending(s => s.ReceptId)
                   .Select(s => new ReceptiVM.Row
                   {
                       naziv = s.Naziv,
                       opis = s.Opis,
                       sastojci = s.Sastojci,
                       vrijemeKuhanja = s.VrijemeKuhanja,
                       receptId = s.ReceptId,
                       slika = s.Slika,
                       level = s.Level

                   }).ToList()
            };


            return model;
        }
        
        [HttpPost("Insert/")]
        public ReceptiVM.Row Insert([FromBody]ReceptiAddVM recept)
        {
            Recepti.WebApi.Database.Recepti receptNovi = new Database.Recepti()
            {
                Naziv = recept.naziv,
                Sastojci = recept.sastojci,
                Opis = recept.opis,
                VrijemeKuhanja = recept.vrijemeKuhanja,
                Level = recept.level,
                KategorijaId = recept.kategorijaId,
                Slika=recept.slika,
                KorisnikId=recept.korisnikId
            };
            _db.Recepti.Add(receptNovi);
            _db.SaveChanges();

            var result = _db.Recepti
                   .Where(w => w.ReceptId == receptNovi.ReceptId)
                   .Select(s => new ReceptiVM.Row
                   {
                       naziv = s.Naziv,
                       opis = s.Opis,
                       sastojci = s.Sastojci,
                       vrijemeKuhanja = s.VrijemeKuhanja,
                       receptId = s.ReceptId,
                       slika = s.Slika

                   })
                    .Single();

            return result;
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromRoute]int id, [FromBody]Recepti.WebApi.Database.Recepti recept)
        {
            if (id != recept.ReceptId)
            {
                return BadRequest();
            }

            _db.Entry(recept).State = EntityState.Modified;
            _db.SaveChanges();

            return NoContent();
        }

    }
}