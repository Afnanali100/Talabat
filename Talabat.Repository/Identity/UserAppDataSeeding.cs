using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class UserAppDataSeeding
    {
        public static async Task AddUserSeedingAsync(UserManager<UserApp> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new UserApp()
                {
                    DisplayName = "Afnan Ali",
                    Email = "afnanamer100@gamil.com",
                    UserName = "afnanamer100",
                    PhoneNumber = "01201305203"
                };

              var result=  await userManager.CreateAsync(user, "P@swo0rd");
            }       
        }


    }
}
