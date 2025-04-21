using Core.API.Services;
using Core.Components.BaseClassess;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Core.Components.Pages
{
    public partial class Register: CustomComponentBase
    {
        [Inject] 
        private ApiService ApiService { get; set; }

        private RegisterFormModel FormModel { get; set; } = new();
        private string Password { get; set; }
        private int? BirthDay { get; set; }
        private int? BirthMonth { get; set; }
        private int? BirthYear { get; set; }
        private string[] MonthNames { get; set; } = new[]
        {
        "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec",
        "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"
    };

        private string Debug { get; set; } = string.Empty;

        private async Task HandleRegister()
        {

            var error = await ApiService.PostAsync("/api/v1/register-patient", FormModel);

            if(error is not null)
            {
                NavigationManager.NavigateTo("/login");
            }
            else
            {
                Debug = error?.Message;
            }

        }


        private class RegisterFormModel
        {
            [Required(ErrorMessage = "Imię jest wymagane")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Nazwisko jest wymagane")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Email jest wymagany")]
            [EmailAddress(ErrorMessage = "Nieprawidłowy adres email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Hasło jest wymagane")]
            [MinLength(6, ErrorMessage = "Hasło musi mieć co najmniej 6 znaków")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Potwierdzenie hasła jest wymagane")]
            [Compare("Password", ErrorMessage = "Hasła się nie zgadzają")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Data urodzenia jest wymagana")]
            public DateTime? BirthDate { get; set; }
        }
    }
}
