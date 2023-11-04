using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Core.Interfaces
{
    public interface ITokenService
    {
        public  Task<string> CreateToken(UserApp userApp, UserManager<UserApp> userManager);
        
    }
}
