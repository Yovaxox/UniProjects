using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Context
{
    public partial class Orden
    {
        public Orden()
        {
            DetalleOrden = new HashSet<DetalleOrden>();
            RecepcionOrden = new HashSet<RecepcionOrden>();
            //UsuarioOrden = new HashSet<UsuarioOrden>();
        }

        public int Id { get; set; }
        public DateTime SolicitadoEl { get; set; }
        public DateTime? ActualizadoEl { get; set; }
        public bool FueRecepcionada { get; set; }
        [Display(Name = "Detalle de la órden")]
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
        public virtual ICollection<RecepcionOrden> RecepcionOrden { get; set; }
        //public virtual ICollection<UsuarioOrden> UsuarioOrden { get; set; }
    }
}
