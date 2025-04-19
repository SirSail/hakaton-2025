using Application.Authorization.Exceptions;
using Domain.Models;
using Infrastructure.DatabaseAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Authorize.Services
{
    public class AuthenticateService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PasswordEncrypterService _passwordEncrypter;
        private readonly IConfiguration _configuration;

        public AuthenticateService(UnitOfWork unitOfWork, IConfiguration configuration, PasswordEncrypterService passwordEncrypter)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _passwordEncrypter = passwordEncrypter;
        }

        public async Task<string> AuthenticateUser(string email, string password)
        {
            SystemUser user = _unitOfWork.UserRepository.FindBy(x => x.Email == email);

            if(user is null)
            {
                BadCredentialsExceptionHelper.ThrowEmailNotFound(email);
            }

            bool isValidPassword = _passwordEncrypter.VerifyPassword(user.PasswordHash, password);
            if (!isValidPassword)
            {
                BadCredentialsExceptionHelper.ThrowInvalidPassword();
            }

            return await generateJwtToken(user);
        }


        private async Task<string> generateJwtToken(SystemUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {

                var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);
        }
    }
}

