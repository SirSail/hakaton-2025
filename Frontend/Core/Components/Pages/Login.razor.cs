using Core.API;
using Core.API.Requests;
using Core.Authorize.Services;
using Core.Components.BaseClassess;
using Microsoft.AspNetCore.Components;

namespace Core.Components.Pages
{
    public partial class Login : CustomComponentBase
    {
        [Inject]
        private AuthService AuthService { get; set; }

        private LoginRequest LoginRequest { get; set; } = new();
        private string PasswordFieldType { get; set; } = "password";
        private bool IsHiddenErrorDialog { get; set; } = true;
        private string ErrorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if(IsLogged)
            {
                NavigationManager.NavigateTo("/",true);
            }
        }


        private async Task HandleLogin()
        {
            try
            {
                LoginRequest.Email = LoginRequest.Email.Trim();    
                ApiError apiError = await AuthService.LoginAsync(LoginRequest);
                if (apiError is not null)
                {
                    ErrorMessage = apiError.Message;
                    IsHiddenErrorDialog = false;
                    await InvokeAsync(StateHasChanged);
                    return;
                }
                NavigationManager.NavigateTo("/", true);

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                IsHiddenErrorDialog = false;
            }
        }

        protected void TogglePasswordVisibility()
        {
            PasswordFieldType = PasswordFieldType ==  "password" ? "text" : "password";
        }

        private Task CloseDialog()
        {
            IsHiddenErrorDialog = false;
            ErrorMessage = string.Empty;

            return Task.CompletedTask;
        }

    }
}