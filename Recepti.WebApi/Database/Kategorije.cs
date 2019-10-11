using System;
using System.Collections.Generic;

namespace Recepti.WebApi.Database
{
    public partial class Kategorije
    {
        public Kategorije()
        {
            Recepti = new HashSet<Recepti>();
        }

        public int KategorijaId { get; set; }
        public string Naziv { get; set; }

        public ICollection<Recepti> Recepti { get; set; }
    }
}
