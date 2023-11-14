using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LindaSonrisa.Models.Context
{
    public partial class Modulo
    {
        public Modulo()
        {
            Reserva = new HashSet<Reserva>();
        }

        [Display(Name = "Módulo")]
        public int Id { get; set; }

        [Display(Name = "Hora de inicio")]
        public string HoraInicio { get; set; }

        [Display(Name = "Hora de término")]
        public string HoraTermino { get; set; }

        public long Box { get; set; }

        [Display(Name = "Estado")]
        public char Disponible { get; set; }

        [Display(Name = "Odontólogo")]
        public int UsuarioId { get; set; }

        [Display(Name = "Servicio")]
        public int ServicioId { get; set; }

        [Display(Name = "Día")]
        public int DiaId { get; set; }

        public virtual Dia Dia { get; set; }
        public virtual Servicio Servicio { get; set; }
        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Reserva> Reserva { get; set; }
    }
}
