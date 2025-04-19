using Application.Authorize.Services;
using Application.Calendar.Services;
using Application.Patients.Services;
using Application.SystemUsers.Services;
using Infrastructure.DatabaseAbstractions;

namespace API
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddCustomServiceInjections(this IServiceCollection services)
        {
            services.AddSingleton<PasswordEncrypterService>();
            services.AddScoped<UnitOfWork>();
           
            services.AddTransient<AuthenticateService>();
            services.AddTransient<UserService>();
            services.AddTransient<PatientService>();
            services.AddTransient<CalendarService>();


            return services;
        }
    }
}
