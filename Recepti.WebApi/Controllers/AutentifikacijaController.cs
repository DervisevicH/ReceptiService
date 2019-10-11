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
    public class AutentifikacijaController : ControllerBase
    {
        private ReceptiContext _db;
        public AutentifikacijaController(ReceptiContext db) 
        {
            _db = db;
        }
        [HttpPost("LoginCheck/")]
        public ActionResult<AutentifikacijaResultVM> LoginCheck([FromBody] AutentifikacijaLoginPostVM korisnik)
        {

            AutentifikacijaResultVM model = _db.Korisnici
                .Where(w => w.Username == korisnik.username && w.Password == korisnik.password)
                .Select(s => new AutentifikacijaResultVM
                {
                   username=s.Username,email=s.Mail,korisnikId=s.KorisnikId
                }).SingleOrDefault();
                       

            return model;
        }




    }
}
