using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Interfaces;
using Talabat.Dtos;
using Talabat.Errors;

namespace Talabat.Controllers
{
   
    public class AccountController : ApiBaseController
    {
        private readonly SignInManager<UserApp> signInManager;
        private readonly ITokenService tokenService;

        public AccountController(UserManager<UserApp> userManager, SignInManager<UserApp> signInManager,ITokenService tokenService)
        {
            UserManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        public UserManager<UserApp> UserManager { get; }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto LoginUser)
        {
            var user = await UserManager.FindByEmailAsync(LoginUser.Email);
            if (user is null) return Unauthorized(new ApiErrorResponde(401));

            var result = await signInManager.CheckPasswordSignInAsync(user, LoginUser.Password, false);
            if(!result.Succeeded) { return Unauthorized(new ApiErrorResponde(401)); }
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateToken(user, UserManager)
            }) ;

        }

        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new UserApp()
            {
                DisplayName=model.DisplayName,
                Email=model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber=model.PhoneNumber
            };

            var result = await UserManager.CreateAsync(user);
            if(!result.Succeeded)
            {
                return BadRequest(new ApiErrorResponde(401));
            }

            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName,
                Email=user.Email,
                Token = await tokenService.CreateToken(user, UserManager)
            });

        } 



    }
}
