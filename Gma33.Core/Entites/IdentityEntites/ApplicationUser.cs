using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Entites.IdentityEntites
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }

        public Address? Address { get; set; }

    }
}
