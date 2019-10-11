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
    public class KomentariController : ControllerBase
    {
        private readonly ReceptiContext _db;

        public KomentariController(ReceptiContext context)
        {
            _db = context;
        }
        [HttpGet("GetKomentariByRecept/{receptId}")]
        public ActionResult<KomentariVM> GetKomentariByRecept([FromRoute]int receptId)
        {
            var model = new KomentariVM
            {
                rows = _db.Komentari.Where(x=>x.ReceptId==receptId)
                   .OrderByDescending(s => s.KomentarId)
                   .Select(s => new KomentariVM.Row
                   {
                       komentar=s.Komentar, receptId=s.ReceptId.Value,komentarId=s.KomentarId, korisnik=s.Korisnik.Username,datumObjave=s.DatumObjave.Value.ToShortDateString()

                   }).ToList()
            };
            return model;
        }
        [HttpGet("{id}")]
        public ActionResult<Komentari> GetById([FromRoute]int id)
        {
            var komentar = _db.Komentari.Find(id);
            if (komentar == null)
            {
                return NotFound();
            }
            else
                return komentar;
        }
        [HttpPost("Insert/")]
        public ActionResult<KomentariVM> Insert([FromBody]KomentariAddVM komentar)
        {
            Komentari newKomentar = new Komentari() { Komentar = komentar.komentar, KorisnikId = komentar.korisnikId,ReceptId=komentar.receptId,DatumObjave=System.DateTime.Now };
            _db.Komentari.Add(newKomentar);
            _db.SaveChanges();
            var model = new KomentariVM
            {
                rows = _db.Komentari.Where(x => x.ReceptId == komentar.receptId)
                  .OrderByDescending(s => s.KomentarId)
                  .Select(s => new KomentariVM.Row
                  {
                      komentar = s.Komentar,
                      receptId = s.ReceptId.Value,
                      komentarId = s.KomentarId,
                      korisnik = s.Korisnik.Username,
                      datumObjave = s.DatumObjave.Value.ToShortDateString()


                  }).ToList()
            };
            return model;
        }
    }
}