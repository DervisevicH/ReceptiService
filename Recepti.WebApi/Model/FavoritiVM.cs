using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.WebApi.Model
{
    public class FavoritiVM
    {
        public class Row
        {
            public int favoritId;
            public int receptId;
            public int korisnikId;
            public string naziv;
            public string vrijemeKuhanja;
            public string sastojci;
            public string opis;
            public string slika;
            public string level;
        }

        public List<Row> rows;
    }
}
