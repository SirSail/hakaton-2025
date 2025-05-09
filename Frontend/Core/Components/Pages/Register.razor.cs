using Core.API.Requests;
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
        private string ErrorMessage { get; set; } = string.Empty;

        private bool IsLoading { get; set; } = false;
        private bool IsHiddenErrorDialog { get; set; } = true;
        private async Task HandleRegister()
        {
            IsLoading = true;

            try
            {
                RegisterRequest registerRequest = new()
                {
                    FirstName = FormModel.FirstName,
                    LastName = FormModel.LastName,
                    Email = FormModel.Email.Trim(),
                    Password = FormModel.Password,
                    BirthDate = FormModel.BirthDate.Value
                };

                var error = await ApiService.PostAsync("api/v1/register-patient", registerRequest);

                if (error is null)
                {
                    NavigationManager.NavigateTo("/login");
                }
                else
                {
                    ErrorMessage = error?.Message ?? "Wystąpił nieznany błąd.";
                    IsHiddenErrorDialog = false;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                IsHiddenErrorDialog = false;
            }
            finally
            {
                IsLoading = false;
            }
        }
        private Task CloseDialog()
        {
            IsHiddenErrorDialog = true;
            ErrorMessage = string.Empty;
            return Task.CompletedTask;
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
            public DateOnly? BirthDate { get; set; }
        }
    }
}
