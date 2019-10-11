using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.WebApi.Model
{
    public class ReceptiAddVM
    {
        public string naziv;
        public string sastojci;
        public string opis;
        public string level;
        public int kategorijaId;
        public string vrijemeKuhanja;
        public int korisnikId;
        public string slika;
    }
}
