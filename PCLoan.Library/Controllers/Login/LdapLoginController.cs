using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PCLoan.Data.Library.Repositorys;
using PCLoan.Data.Library.Models;
using PCLoan.Logic.Library.Models;
using PCLoan.Logic.Library.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PCLoan.Logic.Library.Controllers
{
    public class LdapLoginController : ILoginController
    {
        #region Private Fields

        private IAuthenticationService _authenticationService;

        private IAuthorizationService _authorizationService;

        private ILogger<LdapLoginController> _logger;

        private IMapper _mapper;

        private IUserRepository _userRepository;

        private IConfiguration _configuration;

        #endregion

        #region Public Properties

        #endregion

        #region Constructors

        public LdapLoginController(IAuthenticationService authenticationService,
                                   IAuthorizationService authorizationService, ILogger<LdapLoginController> logger,
                                   IMapper mapper, IUserRepository userRepository, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        #endregion

        #region Public Methods

        public UserModelDTO LoginUser(UserModelDTO model)
        {
            model = _authenticationService.AuthenticateUser(model);

            if (!model.Authenticated)
                return model;

            model = _authorizationService.AuthorizeUser(model);

            // If the user dosnt exsist
            if (!_userRepository.Exsist(model.UserName))
            {
                // Add the user to the database
                _userRepository.Insert(_mapper.Map<UserModelDAO>(model));
            }

            model.Id = _userRepository.GetIdByname(model.UserName);

            model = GenerateJSONWebToken(model);

            model.Password = string.Empty;

            return model;
        }

        #endregion

        #region Private Helper Methods

        private UserModelDTO GenerateJSONWebToken(UserModelDTO model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings")["Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Role, model.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            model.Token = tokenHandler.WriteToken(token);

            return model;
        }

        #endregion
    }
}
