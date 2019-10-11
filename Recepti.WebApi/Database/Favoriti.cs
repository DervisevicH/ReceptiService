using System;
using System.Collections.Generic;

namespace Recepti.WebApi.Database
{
    public partial class Favoriti
    {
        public int FavoritId { get; set; }
        public int? KorisnikId { get; set; }
        public int? ReceptId { get; set; }

        public Korisnici Korisnik { get; set; }
        public Recepti Recept { get; set; }
    }
}
