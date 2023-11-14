using System;
using System.Collections.Generic;

namespace LindaSonrisa.Models.Context
{
    public partial class Dia
    {
        public Dia()
        {
            Modulo = new HashSet<Modulo>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }

        public virtual ICollection<Modulo> Modulo { get; set; }
    }
}
