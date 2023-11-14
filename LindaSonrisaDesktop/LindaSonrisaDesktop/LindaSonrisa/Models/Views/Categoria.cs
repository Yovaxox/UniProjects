using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Views
{
    public class Categoria
    {
        [Display(Name = "N°")]
        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "El título es requerido.")]
        [StringLength(maximumLength:50,ErrorMessage = "El límite de caracteres para el título es de 50.")]
        public string Titulo { get; set; }
        public string TituloNormalizado { get; set; }
        public int CategoriaId { get; set; }

        [Display(Name = "Familia de producto")]
        public int? FamiliaProductoId { get; set; }
    }
}
