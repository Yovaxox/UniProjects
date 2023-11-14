using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Context
{
    public partial class FamiliaProducto
    {
        public FamiliaProducto()
        {
            TipoProducto = new HashSet<TipoProducto>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string TituloNormalizado { get; set; }
        public virtual ICollection<TipoProducto> TipoProducto { get; set; }
    }
}
