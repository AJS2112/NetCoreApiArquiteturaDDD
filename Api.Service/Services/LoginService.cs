
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _repository;
        private SigningConfiguration _signingConfiguration;
        private TokenConfiguration _tokenConfiguration;

        public IConfiguration _configuration { get; }

        public LoginService(IUserRepository repository,
                                SigningConfiguration signingConfiguration,
                                TokenConfiguration tokenConfiguration,
                                IConfiguration configuration)
        {
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<object> FindByLogin(LoginDTO user)
        {
            var baseUser = new UserEntity();

            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _repository.FindByLogin(user.Email);
                if (baseUser == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, baseUser.Email),
                        }
                    );

                    var createDate = DateTime.Now;
                    var expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    var token = CreateToken(identity, createDate, expirationDate, handler);

                    return SuccessObject(createDate, expirationDate, token, user);
                }
            }
            else
            {
                return null;
            }

        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer=_tokenConfiguration.Issuer,
                Audience=_tokenConfiguration.Audience,
                SigningCredentials=_signingConfiguration.SigningCredentials,
                Subject=identity,
                NotBefore=createDate,
                Expires=expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDTO user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expirationDate=expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken=token,
                userName=user.Email,
                message="Usuario logado com sucesso"
            }
        }
    }
}