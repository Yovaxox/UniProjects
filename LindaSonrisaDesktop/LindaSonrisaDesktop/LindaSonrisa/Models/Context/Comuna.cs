using System;
using System.Collections.Generic;

namespace LindaSonrisa.Models.Context
{
    public partial class Comuna
    {
        public Comuna()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
