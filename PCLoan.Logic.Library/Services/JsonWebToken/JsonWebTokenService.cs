using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PCLoan.Logic.Library.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PCLoan.Logic.Library.Services
{
    public class JsonWebTokenService : IJsonWebTokenService
    {
        #region Private Fields

        private IConfiguration _configuration;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public JsonWebTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Public Methods

        public string GenerateJsonWebToken(UserModelDTO model)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            Claim[] claims = new Claim[]
            {
                new Claim("Id", model.Id.ToString()),
                new Claim("Username", model.UserName),
                new Claim("Role", model.Role)
            };

            JwtSecurityToken securityToken = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims, notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        #endregion

        #region Private Helper Methods

        #endregion
    }
}
