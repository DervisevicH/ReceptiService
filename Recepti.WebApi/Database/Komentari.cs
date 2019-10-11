using System;
using System.Collections.Generic;

namespace Recepti.WebApi.Database
{
    public partial class Komentari
    {
        public int KomentarId { get; set; }
        public string Komentar { get; set; }
        public int? KorisnikId { get; set; }
        public int? ReceptId { get; set; }
        public DateTime? DatumObjave { get; set; }

        public Korisnici Korisnik { get; set; }
        public Recepti Recept { get; set; }
    }
}
