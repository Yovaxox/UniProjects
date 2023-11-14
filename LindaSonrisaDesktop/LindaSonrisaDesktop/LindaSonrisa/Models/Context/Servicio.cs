using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LindaSonrisa.Models.Context
{
    public partial class Servicio
    {
        public Servicio()
        {

            Modulo = new HashSet<Modulo>();
            //RegistroServicio = new HashSet<RegistroServicio>();
        }

        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "El título es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El límite de caracteres para el título es de 50.")]
        public string Titulo { get; set; }

        public string TituloNormalizado { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es requerido.")]
        [StringLength(maximumLength: 250, ErrorMessage = "El límite de caracteres para la descripción es de 250.")]
        [DefaultValue(0)]
        public string Descripcion { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El precio es requerido.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El precio acepta solo caracteres numéricos.")]
        [Range(0, 999999999, ErrorMessage = "El rango del precio es de 0 a 999999999.")]
        [DefaultValue(0)]
        public int Precio { get; set; }

        [Display(Name = "Estado")]
        public char EstaInactivo { get; set; }

        [Display(Name = "Promoción")]
        [Required(ErrorMessage = "Se requiere saber si el producto es promoción.")]
        public char EsPromocion { get; set; }

        [Display(Name = "Descuento")]
        [Required(ErrorMessage = "El descuento es requerido.")]
        public decimal Descuento { get; set; }

        [Display(Name = "Comentario")]
        public string Comentario { get; set; }

        public string FileId { get; set; }

        public virtual ICollection<Modulo> Modulo { get; set; }
        //public virtual ICollection<RegistroServicio> RegistroServicio { get; set; }
    }
}
