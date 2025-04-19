using Domain.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Domain.Models.Enums;

namespace API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private List<Role> _requiredRoles = [];

        public AuthorizeAttribute()
        {
            _requiredRoles = [];
        }

        public AuthorizeAttribute(Role requiredRoleId)
        {
            _requiredRoles = [requiredRoleId];
        }
        public AuthorizeAttribute(params Role[] requiredRoleids)
        {
            _requiredRoles = [.. requiredRoleids];
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (SystemUser?)context.HttpContext.Items["User"];
            if (user is null)
            {
                context.Result = new JsonResult(new { message = "Niezautoryzowano" }) { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            if (_requiredRoles.Count > 0 && !_requiredRoles.Contains(user.Role))
            {
                context.Result = new JsonResult(new { message = "Użytkownik nie ma dostępu do tego endpointa" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}
