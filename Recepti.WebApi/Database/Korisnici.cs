using System;
using System.Collections.Generic;

namespace Recepti.WebApi.Database
{
    public partial class Korisnici
    {
        public Korisnici()
        {
            Favoriti = new HashSet<Favoriti>();
            Komentari = new HashSet<Komentari>();
            Recepti = new HashSet<Recepti>();
        }

        public int KorisnikId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }

        public ICollection<Favoriti> Favoriti { get; set; }
        public ICollection<Komentari> Komentari { get; set; }
        public ICollection<Recepti> Recepti { get; set; }
    }
}
