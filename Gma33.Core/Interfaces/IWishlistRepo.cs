using Gma33.Core.Entites.StoreEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Interfaces
{
    public interface IWishlistRepo
    {
        Task<Wishlist> GetWishlistAsync(string buyerEmail);
        Task<Wishlist> CreateOrUpdateWishlistAsync(Wishlist wishlist); // For Update and Create
        Task<bool> DeleteWishlistAsync(string buyerEmail);
    }
}
