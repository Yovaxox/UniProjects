using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Context
{
    public partial class Reserva
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime SolicitadoEl { get; set; }

        [Display(Name = "Anulado")]
        public char FueAnulada { get; set; }

        [Display(Name = "Fecha reservada")]
        [DataType(DataType.Date)]
        public DateTime FechaReserva { get; set; }
        public DateTime? FechaAnulacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        [Display(Name = "Hora reservada")]
        public string Hora { get; set; }

        [Display(Name = "Cliente")]
        public int UsuarioId { get; set; }

        [Display(Name = "Módulo")]
        public int ModuloId { get; set; }

        public virtual Modulo Modulo { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
