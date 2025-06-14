using Gma33.Core.Entites.IdentityEntites;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.Identity
{
    public class IdentityDBContextSeed
    {
        public static async Task IdentityDataSeed(UserManager<ApplicationUser> userManager)
        {



            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {


                    DisplayName = "FadyMelad",
                    Email = "fmelad86@gmail.com",
                    UserName = "FadyMeladAdmin",
                    PhoneNumber = "01019022358"
                };

                await userManager.CreateAsync(user, "FadyMelad98!@#");

            }


        }
    }
}
