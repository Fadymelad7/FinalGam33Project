using Gma33.Core.Entites.IdentityEntites;
using Gma33.Core.Interfaces.IdentityServicesInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Services.TokenServices
{
    public class TokenServices : IToken
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> GetTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>() {

            new Claim(ClaimTypes.GivenName,user.DisplayName),

            new Claim(ClaimTypes.Email,user.Email),

            new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtToken:Key"]));

            var Token = new JwtSecurityToken(

                issuer: _configuration["JwtToken:Issuer"],
                audience: _configuration["JwtToken:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JwtToken:ExpaireDate"]!)),
                claims: Claims,
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature) // is what you use to sign the JWT with your secret key and a chosen algorithm
                );
            var JwtString = new JwtSecurityTokenHandler().WriteToken(Token);


            return Task.FromResult(JwtString);

        }

    }
}

