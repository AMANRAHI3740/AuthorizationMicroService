using AuthorizationMicroService.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Services
{
    public class AuthorizationServies : IAuthorizationServies
    {
        public readonly AppSettings _appSettings;
        public AuthorizationServies(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        private List<User> users = new List<User>()
        {
            new User{ UserId = 1001 ,Password="12345"},
            new User{ UserId = 1002 ,Password="12345"},
            new User{ UserId = 1003 ,Password="12345"},
            new User{ UserId = 1004 ,Password="12345"}
        };
        public string Authorization(int UserId, string Password)
        {
            var user = users.SingleOrDefault(x => x.UserId == UserId && x.Password == Password);

            //return null if user is not found
            if (user == null)
                return null;
            var tokenHandeler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.UserId.ToString()),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim(ClaimTypes.Version,"V3.1")
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials=new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandeler.CreateToken(tokenDescriptor);
            user.Token = tokenHandeler.WriteToken(token);

            user.Password = null;
            return user.Token;
        }
    }
}
