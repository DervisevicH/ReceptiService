using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.WebApi.Model
{
    public class ReceptiVM
    {
        public class Row
        {
            public int receptId;
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
