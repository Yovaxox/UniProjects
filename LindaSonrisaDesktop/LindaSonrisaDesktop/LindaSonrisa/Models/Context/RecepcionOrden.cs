using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Context
{
    public partial class RecepcionOrden
    {
        public int Id { get; set; }
        public string Codificacion { get; set; }
        public DateTime RecepcionadoEl { get; set; }
        public int Cantidad { get; set; }
        public string Producto { get; set; }
        public int ProductoId { get; set; }
        public int OrdenId { get; set; }
        public virtual Orden Orden { get; set; }
    }
}
