using Gma33.Core.Entites.StoreEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Interfaces
{
    public interface ICartRepo
    {
        Task<Cart?> GetCartAsync(string userEmail);
        Task<Cart> CreateOrUpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(string userEmail);
    }
}
