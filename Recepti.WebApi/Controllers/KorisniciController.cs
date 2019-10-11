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
    public class KorisniciController : ControllerBase
    {

        private readonly ReceptiContext _db;

        public KorisniciController(ReceptiContext context)
        {
            _db = context;
        }

        [HttpGet("{id}")]
        public ActionResult<Korisnici> GetById(int id)
        {
            var korisnik =  _db.Korisnici.Find(id);

            if (korisnik == null)
            {
                return NotFound();
            }
            else
            return korisnik;
        }
        [HttpPost("Insert/")]
        public ActionResult<AutentifikacijaResultVM> Insert([FromBody]KorisniciAddVM korisnik)
        {
            Korisnici newKorisnik = new Korisnici
            {
                Mail = korisnik.mail,
                Username=korisnik.username,
                Password=korisnik.password
            };            

            _db.Korisnici.Add(newKorisnik);
            _db.SaveChanges();

            AutentifikacijaResultVM model = _db.Korisnici
                .Where(w => w.Username == korisnik.username && w.Password == korisnik.password)
                .Select(s => new AutentifikacijaResultVM
                {
                    username = s.Username,
                    email = s.Mail,
                    korisnikId = s.KorisnikId
                }).SingleOrDefault();


            return model;
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, Korisnici korisnik)
        {
            if (id != korisnik.KorisnikId)
            {
                return BadRequest();
            }

            _db.Entry(korisnik).State = EntityState.Modified;
            _db.SaveChanges();

            return NoContent();
        }
    }
}