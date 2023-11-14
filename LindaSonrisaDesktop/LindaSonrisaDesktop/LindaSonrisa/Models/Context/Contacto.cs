using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Context
{
    public partial class Contacto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El límite de caracteres para el nombre es de 50.")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido paterno")]
        [StringLength(maximumLength: 50, ErrorMessage = "El límite de caracteres para el apellido paterno es de 50.")]
        public string ApPaterno { get; set; }

        [Display(Name = "Apellido materno")]
        [StringLength(maximumLength: 50, ErrorMessage = "El límite de caracteres para el apellido materno es de 50.")]
        public string ApMaterno { get; set; }

        [Required(ErrorMessage = "El telefono móvil es requerido.")]
        [Display(Name = "Telefono móvil")]
        [StringLength(maximumLength: 8, MinimumLength = 8, ErrorMessage = "El telefono móvil debe tener 8 números.")]
        public string FonoMovil { get; set; }

        [EmailAddress]
        [StringLength(maximumLength: 50, ErrorMessage = "El límite de caracteres para el email es de 50.")]
        public string Email { get; set; }

        [Display(Name = "Proveedor")]
        public int ProveedorId { get; set; }

        public virtual Proveedor Proveedor { get; set; }
    }
}
