using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Context
{
    public partial class DetalleOrden
    {
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
