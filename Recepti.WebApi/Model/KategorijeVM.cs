using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recepti.WebApi.Model
{
    public class KategorijeVM
    {
        public class Row
        {
            public int kategorijaId;
            public string naziv;
        }

        public List<KategorijeVM.Row> rows;
    }
}
