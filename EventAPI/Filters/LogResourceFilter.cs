using Microsoft.AspNetCore.Mvc.Filters;

namespace EventAPI.Filters
{
    public class LogResourceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("Filtro de Resource LogResourceFilter (APÓS) OnResourceExecuted");
            Console.WriteLine("-------------------------------------");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.Keys.Contains("Code"))
            {
                context.HttpContext.Request.Headers.Add("Code", Guid.NewGuid().ToString());
            }

            Console.WriteLine("Filtro de Resource LogResourceFilter (ANTES) OnResourceExecuting");
        }
    }
}
