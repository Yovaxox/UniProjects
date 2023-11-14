using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LindaSonrisa.Models.Context
{
    public partial class AuthUser
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsSuperUser { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsStaff { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateJoined { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
