using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Context
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Contacto = new HashSet<Contacto>();
            Producto = new HashSet<Producto>();
        }

        [Display(Name = "N°")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El límite de caracteres para el nombre es de 50.")]
        public string Nombre { get; set; }

        public string NombreNormalizado { get; set; }

        [Display(Name = "Telefono fijo")]
        [Required(ErrorMessage = "El telefono fijo es requerido.")]
        [RegularExpression(@"^[0-9]{8,8}$", ErrorMessage = "El telefono fijo solo puede contener 8 caracteres numéricos.")]
        public string FonoFijo { get; set; }

        [Display(Name = "Telefono móvil")]
        [RegularExpression(@"^[0-9]{8,8}$", ErrorMessage = "El telefono móvil solo puede contener 8 caracteres numéricos.")]
        public string FonoMovil { get; set; }

        [Required(ErrorMessage = "El email es requerido.")]
        [EmailAddress(ErrorMessage = "El email no tiene el formato correcto")]
        [StringLength(maximumLength: 50, ErrorMessage = "El límite de caracteres para el email es de 50.")]
        public string Email { get; set; }

        [Display(Name = "Dirección web")]
        [StringLength(maximumLength: 50, ErrorMessage = "El límite de caracteres para la dirección web es de 50.")]
        public string Url { get; set; }

        [Display(Name = "Comentario")]
        [StringLength(maximumLength: 250, ErrorMessage = "El límite de caracteres para el comentario es de 250.")]
        public string Comentario { get; set; }

        [Display(Name = "Estado")]
        public bool EstaInactivo { get; set; }

        [Display(Name = "Rubro")]
        public int RubroId { get; set; }

        public virtual Rubro Rubro { get; set; }
        public virtual ICollection<Contacto> Contacto { get; set; }
        public virtual ICollection<Producto> Producto { get; set; }
    }
}
