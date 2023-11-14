using Microsoft.AspNetCore.Identity;
using LindaSonrisa.Models.Context;

namespace LindaSonrisa.Models.Identity
{
    public partial class User : IdentityUser
    {
        public virtual Usuario Usuario { get; set; }
    }
}
