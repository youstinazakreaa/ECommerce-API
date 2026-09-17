using Microsoft.AspNetCore.Identity;
using onine.core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace onine.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "youstina zakrea",
                    Email = "youstinazakrea@gmail.com",
                    PhoneNumber = "01152042826",
                    UserName = "youstinazakrea"
                };

                var result = await userManager.CreateAsync(user, "Pa$$word1");

            }

        }
    }
}
