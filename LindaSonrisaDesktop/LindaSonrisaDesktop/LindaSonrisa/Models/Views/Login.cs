using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Views
{
    public class Login
    {
        [Required(ErrorMessage = "El RUT es requerido.")]
        [StringLength(maximumLength:12, MinimumLength = 11, ErrorMessage = "El largo del RUT no es válido. Revise el formato.")]
        [RegularExpression(@"[Kk0-9\.-]+", ErrorMessage = "El RUT ingresado no cumple con el formato solicitado.")]
        [Display(Name = "RUT")]
        public string Rut { get; set; }

        [Required (ErrorMessage = "El campo contraseña es requerida.")]
        [DataType (DataType.Password)]
        [Display (Name = "Contraseña")]
        [StringLength(maximumLength: 50, ErrorMessage = "El límite de caracteres para la contraseña es de 50.")]
        public string Password { get; set; }

        /*[Display (Name = "Recuérdame")]
        public bool Recuerdame { get; set; }*/
    }
}
