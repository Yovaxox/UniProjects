using LindaSonrisa.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Views
{
    public class Empleado : Usuario
    {

        [Required(ErrorMessage = "El RUN es requerido.")]
        //[StringLength(maximumLength:9, MinimumLength = 8, ErrorMessage = "El largo del rut no es válido. Revise el formato.")]
        [RegularExpression(@"[Kk0-9\.-]+", ErrorMessage = "El RUN solo puede contener dígitos (0-9) y la letra K")]
        [Display(Name = "Rol único nacional")]
        public string Run { get; set; }

        [Required(ErrorMessage = "El email es requerido.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "El email ingresado es incorrecto.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El limite de caracteres para el email es de 50.")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 50, ErrorMessage = "El limite de caracteres para la contraseña es de 50.")]
        public string Password { get; set; }

        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 50, ErrorMessage = "El limite de caracteres para la confirmación de la contraseña es de 50.")]
        [Compare("Password", ErrorMessage = "Las contraseñas ingresadas no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "El telefono móvil es requerido.")]
        [Display(Name = "Telefono móvil")]
        [RegularExpression(@"^[0-9]{8,8}$", ErrorMessage = "El telefono móvil solo puede contener 8 caracteres numéricos")]
        public override string FonoMovil { get; set; }

        [Display(Name = "Estado civil")]
        [Required(ErrorMessage = "El estado civil es requerido.")]
        public override int? EstadoCivilId { get; set; }

        [Display(Name = "Región")]
        public int RegionId { get; set; }

        [Display(Name = "Rol")]
        public string RoleId { get; set; }

        public string StringOfCreadoEl { get; set; }
        public string StringOfActualizadoEl { get; set; }
    }
}
