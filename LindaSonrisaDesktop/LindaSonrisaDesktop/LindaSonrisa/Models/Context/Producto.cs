using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Context
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleOrden = new HashSet<DetalleOrden>();
        }

        public int Id { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "El título es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El límite de caracteres para el nombre es de 50.")]
        public string Titulo { get; set; }

        public string TituloNormalizado { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es requerido.")]
        [StringLength(maximumLength: 250, ErrorMessage = "El límite de caracteres para la descripción es de 250.")]
        [DefaultValue(0)]
        public string Descripcion { get; set; }

        [Display(Name = "Stock crítico")]
        [Required(ErrorMessage = "El stock crítico es requerido.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "El stock crítico acepta solo caracteres numéricos.")]
        [Range(0, 999999999, ErrorMessage = "El rango del stock crítico es de 0 a 999999999.")]
        [DefaultValue(0)]
        public int StockCritico { get; set; }

        [Display(Name = "Promoción")]
        [Required(ErrorMessage = "Se requiere saber si el producto es promoción.")]
        [DefaultValue(false)]
        public char EsPromocion { get; set; }

        [Display(Name = "Descuento")]
        [Required(ErrorMessage = "El descuento es requerido.")]
        public decimal Descuento { get; set; }

        [Display(Name = "Comentario")]
        [StringLength(maximumLength: 250, ErrorMessage = "El límite de caracteres para la descripción es de 250.")]
        public string Comentario { get; set; }

        [Display(Name = "Estado")]
        public bool EstaInactivo { get; set; }

        public string FileId { get; set; }

        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "El proveedor es requerido.")]
        public int ProveedorId { get; set; }

        [Display(Name = "Tipo de producto")]
        [Required(ErrorMessage = "El tipo de producto es requerido.")]
        public int TipoProductoId { get; set; }

        public virtual Proveedor Proveedor { get; set; }
        public virtual TipoProducto TipoProducto { get; set; }
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
    }
}
