using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.WebApi.Model
{
    public class KomentariVM
    {
        public class Row
        {
            public int komentarId;
            public string komentar;
            public int receptId;
            public string korisnik;
            public string datumObjave;
        }

        public List<Row> rows;
    }
}
