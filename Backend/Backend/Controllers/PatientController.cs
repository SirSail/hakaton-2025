using API.Requests;
using API.Validations;
using Application.Patients.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("/api/v1/register-patient")]
        public async Task<IResult> RegisterPatient([FromBody] RegisterPatientRequest request)
        {
            BasicValidations.ValidateTextNotEmpty(nameof(request.FirstName), request.FirstName);
            BasicValidations.ValidateTextNotEmpty(nameof(request.LastName), request.LastName);
            BasicValidations.ValidateTextNotEmpty(nameof(request.Email), request.Email);
            BasicValidations.ValidateTextNotEmpty(nameof(request.Password), request.Password);

            BasicValidations.ValidateEmail(nameof(request.Email), request.Email);
            BasicValidations.ValidateTextLength(nameof(request.Password), request.Password,minLength: 8);

            PatientInfo patientInfo = new PatientInfo()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate
            };
            await _patientService.AddNewPatient(request.Email, request.Password, patientInfo);

            return Results.Ok(new
            {
                message = "Pomyślnie zarejestrowano pacjenta"
            });
        }
    }
}
