using API.Requests;
using API.Validations;
using Application.Authorize.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly AuthenticateService _authenticateService;

        public AuthorizeController(AuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpGet("siema")]
        public IResult Siema()
        {
            return Results.Ok(new { message = "siema" });
        }

        [HttpPost("api/v1/authorize")]
        public async Task<IResult> Result([FromBody] AuthorizeRequest request)
        {
            BasicValidations.ValidateTextNotEmpty(nameof(request.Email), request.Email);
            BasicValidations.ValidateTextNotEmpty(nameof(request.Password), request.Email);

            BasicValidations.ValidateTextLength(nameof(request.Password), request.Email, minLength: 8);
            BasicValidations.ValidateEmail(nameof(request.Email), request.Email);


            var token = await _authenticateService.AuthenticateUser(request.Email, request.Password);
            return Results.Ok(new { Token = token });
        }
    }
}
