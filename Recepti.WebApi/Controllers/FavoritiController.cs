using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recepti.WebApi.Database;
using Recepti.WebApi.Model;

namespace Recepti.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritiController : ControllerBase
    {
        private readonly ReceptiContext _db;

        public FavoritiController(ReceptiContext context)
        {
            _db = context;
        }
        [HttpGet("{id}")]
        public ActionResult<Favoriti> GetById([FromRoute]int id)
        {
            var favoriti = _db.Favoriti.Find(id);
            if (favoriti == null)
            {
                return NotFound();
            }
            else
                return favoriti;
        }
        [HttpGet("GetByKorisnik/{id}")]
        public ActionResult<FavoritiVM> GetByKorisnik([FromRoute]int id) {
            
            var model = new FavoritiVM
            {
                rows = _db.Favoriti.Where(x=>x.KorisnikId==id)                      
                      .Select(s => new FavoritiVM.Row
                      {
                          naziv = s.Recept.Naziv,
                          opis = s.Recept.Opis,
                          sastojci = s.Recept.Sastojci,
                          vrijemeKuhanja = s.Recept.VrijemeKuhanja,
                          receptId = s.Recept.ReceptId,
                          favoritId=s.FavoritId,
                          korisnikId=s.KorisnikId.Value,
                          slika=s.Recept.Slika,
                          level=s.Recept.Level

                      }).ToList()
            };
            return model;
        }
        [HttpPost("Insert/")]
        public bool Insert([FromBody] FavoritiAddVM favoriti)
        {
            var favorit = _db.Favoriti.Where(x => x.KorisnikId == favoriti.korisnikId && x.ReceptId == favoriti.receptId).SingleOrDefault();
            if (favorit == null)
            {
                Favoriti newFavorit = new Favoriti
                {
                    ReceptId = favoriti.receptId,
                    KorisnikId = favoriti.korisnikId

                };

                _db.Favoriti.Add(newFavorit);
                _db.SaveChanges();
                return true;
            }
            else
                return false;
           


           
        
        }
    }
}