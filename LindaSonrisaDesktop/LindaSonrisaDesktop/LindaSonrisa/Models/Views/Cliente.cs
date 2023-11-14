using LindaSonrisa.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Views
{
    public class Cliente : Usuario
    {
        [Required(ErrorMessage = "El RUN es requerido.")]
        [RegularExpression(@"[Kk0-9\.-]+", ErrorMessage = "El RUN solo puede contener dígitos (0-9) y la letra K")]
        [Display(Name = "Rol único nacional")]
        public string Run { get; set; }

        [Required(ErrorMessage = "El email es requerido.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "El email ingresado es incorrecto.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El limite de caracteres para el email es de 50.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El telefono móvil es requerido.")]
        [Display(Name = "Telefono móvil")]
        [RegularExpression(@"^[0-9]{8,8}$", ErrorMessage = "El telefono móvil solo puede contener 8 caracteres numéricos")]
        public override string FonoMovil { get; set; }

        [Display(Name = "Estado civil")]
        [Required(ErrorMessage = "El estado civil es requerido.")]
        public override int? EstadoCivilId { get; set; }

        [Display(Name = "Región")]
        public int RegionId { get; set; }
    }
}