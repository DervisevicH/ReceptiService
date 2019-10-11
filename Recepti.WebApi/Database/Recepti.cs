using System;
using System.Collections.Generic;

namespace Recepti.WebApi.Database
{
    public partial class Recepti
    {
        public Recepti()
        {
            Favoriti = new HashSet<Favoriti>();
            Komentari = new HashSet<Komentari>();
        }

        public int ReceptId { get; set; }
        public string Naziv { get; set; }
        public int? KategorijaId { get; set; }
        public string VrijemeKuhanja { get; set; }
        public string Sastojci { get; set; }
        public string Opis { get; set; }
        public string Slika { get; set; }
        public string Level { get; set; }
        public int? KorisnikId { get; set; }

        public Kategorije Kategorija { get; set; }
        public Korisnici Korisnik { get; set; }
        public ICollection<Favoriti> Favoriti { get; set; }
        public ICollection<Komentari> Komentari { get; set; }
    }
}
