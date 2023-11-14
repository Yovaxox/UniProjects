using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using LindaSonrisa.Models.Identity;

namespace LindaSonrisa.Models.Context
{
    public partial class Usuario
    {
        public Usuario()
        {
            //BoletaServicio = new HashSet<BoletaServicio>();
            //DocumentoCliente = new HashSet<DocumentoCliente>();
            Modulo = new HashSet<Modulo>();
            //RegistroServicio = new HashSet<RegistroServicio>();
            Reserva = new HashSet<Reserva>();
            //UsuarioOrden = new HashSet<UsuarioOrden>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El limite de caracteres para el nombre es de 50.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido paterno es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El limite de caracteres para el apellido paterno es de 50.")]
        [Display(Name = "Apellido paterno")]
        public string ApPaterno { get; set; }

        [Required(ErrorMessage = "El apellido materno es requerido.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El limite de caracteres para el apellido materno es de 50.")]
        [Display(Name = "Apellido materno")]
        public string ApMaterno { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es requerido.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento es incorrecta.")]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Telefono fijo")]
        [RegularExpression(@"^[0-9]{8,8}$", ErrorMessage = "El telefono fijo solo puede contener 8 caracteres numéricos.")]
        public string FonoFijo { get; set; }

        public virtual string FonoMovil { get; set; }

        [Required(ErrorMessage = "La dirección es requerida.")]
        [StringLength(maximumLength: 50, ErrorMessage = "El limite de caracteres para la dirección es de 50.")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        public string RutaFoto { get; set; }

        [Display(Name = "Creado")]
        public DateTime? CreadoEl { get; set; }

        [Display(Name = "Actualizado")]
        public DateTime? ActualizadoEl { get; set; }

        [Display(Name = "Extranjero")]
        public bool EsExtranjero { get; set; }

        [Display(Name = "Comuna")]
        [Required(ErrorMessage = "La comuna es requerida.")]
        [BindRequired]
        public int ComunaId { get; set; }

        [Required(ErrorMessage = "El género es requerida.")]
        [Display(Name = "Género")] 
        public int GeneroId { get; set; }

        public virtual int? EstadoCivilId { get; set; }

        [Display(Name = "Usuario")]
        public string UserId { get; set; }

        public long? AuthUserId { get; set; }

        [Display(Name = "Comuna")]
        public virtual Comuna Comuna { get; set; }

        [Display(Name = "Estado civil")]
        public virtual EstadoCivil EstadoCivil { get; set; }

        [Display(Name = "Género")]
        public virtual Genero Genero { get; set; }

        [Display(Name = "Usuario")]
        public virtual User User { get; set; }

        public virtual AuthUser AuthUser { get; set; }

        public virtual ICollection<Modulo> Modulo { get; set; }
        /*
        public virtual ICollection<BoletaServicio> BoletaServicio { get; set; }
        public virtual ICollection<DocumentoCliente> DocumentoCliente { get; set; }
        public virtual ICollection<RegistroServicio> RegistroServicio { get; set; }*/

        public virtual ICollection<Reserva> Reserva { get; set; }
        //public virtual ICollection<UsuarioOrden> UsuarioOrden { get; set; }

    }
}
