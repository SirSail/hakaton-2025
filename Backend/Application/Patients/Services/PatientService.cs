using Application.Authorize.Services;
using Domain.Models;
using Domain.Models.Enums;
using Infrastructure.DatabaseAbstractions;

namespace Application.Patients.Services
{
    public class PatientService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly PasswordEncrypterService _passwordEncrypterService;

        public PatientService(UnitOfWork unitOfWork, PasswordEncrypterService passwordEncrypterService)
        {
            _unitOfWork = unitOfWork;
            _passwordEncrypterService = passwordEncrypterService;
        }


        public Task AddNewPatient(string email,string password, PatientInfo patientInfo)
        {
            string passwordHash = _passwordEncrypterService.HashPassword(password);
            SystemUser user = new SystemUser() { Email = email, Role = Role.Patient, PasswordHash = passwordHash };

            _unitOfWork.UserRepository.Add(user);

            patientInfo.SystemUser = user;
            _unitOfWork.PatientInfoRepository.Add(patientInfo);

            _unitOfWork.Commit();
            return Task.CompletedTask;
        }
    }


}
