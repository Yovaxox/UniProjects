using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Context
{
    public partial class Rubro
    {
        public Rubro()
        {
            Proveedor = new HashSet<Proveedor>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }

        public virtual ICollection<Proveedor> Proveedor { get; set; }
    }
}
