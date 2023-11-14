using System;
using System.Collections.Generic;

namespace LindaSonrisa.Models.Context
{
    public partial class EstadoCivil
    {
        public EstadoCivil()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }

        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
