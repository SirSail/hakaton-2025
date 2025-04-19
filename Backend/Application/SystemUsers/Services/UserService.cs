using Application.Authorize.Services;
using Domain.Models;
using Infrastructure.DatabaseAbstractions;

namespace Application.SystemUsers.Services
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PasswordEncrypterService passwordEncrypterService;

        public UserService(UnitOfWork unitOfWork, PasswordEncrypterService passwordEncrypterService)
        {
            _unitOfWork = unitOfWork;
            this.passwordEncrypterService = passwordEncrypterService;
        }

        
    }
}
