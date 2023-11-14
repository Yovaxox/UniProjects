using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Context
{
    public partial class TipoProducto
    {
        public TipoProducto()
        {
            Producto = new HashSet<Producto>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string TituloNormalizado { get; set; }
        public int FamiliaProductoId { get; set; }

        public virtual FamiliaProducto FamiliaProducto { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
