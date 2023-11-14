using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Views
{
    public class Servicio
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El límite de caracteres para el nombre es de 50.")]
        public string Nombre { get; set; }

        public string NombreNormalizado { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El precio es requerido.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El precio acepta solo caracteres numéricos.")]
        [Range(0, 999999999, ErrorMessage = "El rango del precio es de 0 a 999999999.")]
        [DefaultValue(0)]
        public int Precio { get; set; }

        [Display(Name = "Descuento")]
        [Required(ErrorMessage = "El descuento es requerido.")]
        public decimal Descuento { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es requerido.")]
        [StringLength(maximumLength: 250, ErrorMessage = "El límite de caracteres para la descripción es de 250.")]
        [DefaultValue(0)]
        public string Descripcion { get; set; }

        [Display(Name = "Comentario")]
        public string Comentario { get; set; }

        [Display(Name = "Promoción")]
        [Required(ErrorMessage = "Se requiere saber si el producto es promoción.")]
        [DefaultValue(false)]
        public bool EsPromocion { get; set; }

        [Display(Name = "Estado")]
        public bool EstaInactivo { get; set; }

        [Display(Name = "Imagen")]
        public IFormFile Image { get; set; }

        public bool HasImage { get; set; }
    }
}
