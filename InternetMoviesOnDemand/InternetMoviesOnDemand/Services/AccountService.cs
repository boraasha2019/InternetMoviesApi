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

        /// <summary>
        /// Verify if the user exists, if yes then genrate the token
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public string Login(LoginDto loginDto)
        {
            //check if the user exists in the list
            var user = TemporaryDataContext._users.Where(x => x.UserName == loginDto.Username && x.Password == loginDto.Password).SingleOrDefault();

            if (user == null)
            {
                return null;
            }

            return GenerateToken(user);
        }


        /// <summary>
        /// Method to Generate Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateToken(User user)
        {
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
