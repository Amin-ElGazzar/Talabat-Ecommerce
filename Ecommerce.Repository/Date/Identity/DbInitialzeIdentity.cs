using Ecommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Date.Identity
{
    public class DbInitialzeIdentity
    {
        public static async Task IdentitySeedAsync(UserManager<ApplicationUser> userManger)
        {
            if (!userManger.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Amin ElGazzar",
                    UserName = "Amin.ElGazzar",
                    Email = "AminElGazzar@Gmail.com",
                    PhoneNumber = "01061056714"

                };
                await userManger.CreateAsync(user,"Pas$$w0rd");
            }
        }
    }
}
