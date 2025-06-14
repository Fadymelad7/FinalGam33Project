using Gma33.Core.Entites.IdentityEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Interfaces.IdentityServicesInterfaces
{
    public interface IToken
    {
        Task<string> GetTokenAsync(ApplicationUser user);
    }
}
