using InternetMoviesOnDemand.Data;
using InternetMoviesOnDemand.Dtos;
using InternetMoviesOnDemand.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace InternetMoviesOnDemand.Services
{
    public class AccountService
    {
        private readonly IConfiguration _configuration;

        public AccountService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //method to generate token
        public string Login(LoginDto loginDto)
        {
            var user = TemporaryDataContext._users.Where(x => x.UserName == loginDto.Username && x.Password == loginDto.Password).SingleOrDefault();

            if (user == null)
            {
                return null;
            }

            var signingKey = Convert.FromBase64String(_configuration["Jwt:SigningSecret"]);
            var expiryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("userid", user.Id.ToString()),
                        new Claim("role", user.Role)
                    }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }

    }
}
