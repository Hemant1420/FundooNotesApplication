using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.ContextClass;
using Repository_Layer.Entity;
using Repository_Layer.User_Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Layer.JwtToken
{
    public class Jwt_Token
    {
       


            private readonly IConfiguration _config;
            public Jwt_Token( IConfiguration config)

            {
                _config = config;

            }

        public string GenerateToken(UserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Initializing an array of Claim type objects
            var claims = new[]
            {

               new Claim("Email",user.Email),                   //Creating Claim object that would get stored in Jwt payload
               new Claim("UserId",user.Id.ToString())
                


            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateTokenReset(string email, int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Initializing an array of Claim type objects
            var claims = new[]
            {

               new Claim("Email",email),                   //Creating Claim object that would get stored in Jwt payload
               new Claim("UserId",userId.ToString()),


            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
