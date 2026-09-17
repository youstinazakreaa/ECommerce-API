using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using onine.core.Entites.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace onine.services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user, Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var UserRole = await userManager.GetRolesAsync(user);


            foreach(var Role in UserRole)
            {
                Claims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var Token = new JwtSecurityToken(
              issuer: _configuration["JWT:ValidIssuer"],
              audience: _configuration["JWT:ValidAudience"],
              claims: Claims,
              expires: DateTime.UtcNow.AddDays(
              double.Parse(_configuration["JWT:DurationInDays"])),
             signingCredentials: new SigningCredentials(
             key,
             SecurityAlgorithms.HmacSha256)
    );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

       
    }
}
