using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using onine.core.Entites.Identity;
using onine.services;
using onineproject.DTOs;
using onineproject.Errors;

namespace onineproject.Controllers
{

    public class AccountController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        //Register

        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {

            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.EmailAddress,
                UserName = model.EmailAddress.Split('@')[0],
                PhoneNumber = model.phoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)

                return BadRequest(new ApiResponse(400));

            var returneduser = new UserDto()
            {
                DisplayName = user.DisplayName,
                EmailAddress = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            };

            return Ok(returneduser);


        }


        //login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user == null)
                return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                EmailAddress = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)

            });
    
        }
    }
}
