using System;
using System.Collections.Generic;

namespace LindaSonrisa.Models.Context
{
    public partial class Region
    {
        public Region()
        {
            Comuna = new HashSet<Comuna>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }

        public virtual ICollection<Comuna> Comuna { get; set; }
    }
}
