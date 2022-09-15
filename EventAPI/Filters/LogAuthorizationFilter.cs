using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Filters
{
    public class LogAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Console.WriteLine("Filtro de autorização LogAuthorizationFilter OnAuthorization");
            //context.HttpContext.Request.Headers.TryGetValue("User", out var usuario);
            //if (String.IsNullOrEmpty(usuario))
            //{
            //    context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            //}
        }
    }
}
