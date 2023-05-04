using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MilestoneWebApplication.Controllers
{
    internal class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string username = context.HttpContext.Session.GetString("username");
            if(username == null)
            {
                context.Result = new RedirectResult("/");
            }
            else
            {

            }
        }
    }
}