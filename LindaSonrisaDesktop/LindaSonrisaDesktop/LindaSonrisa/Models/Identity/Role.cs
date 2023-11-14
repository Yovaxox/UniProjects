using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models
{
    public class Role
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [Display(Name = "Nombre")]
        [StringLength(maximumLength:50,ErrorMessage = "El limite de caracteres para el nombre es de 50.")]
        public string Name { get; set; }
    }
}
