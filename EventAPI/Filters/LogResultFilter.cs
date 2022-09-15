using Microsoft.AspNetCore.Mvc.Filters;

namespace EventAPI.Filters
{
    public class LogResultFilter : ResultFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Filtro de Resource LogResultFilter (APÓS) OnResultExecuted");
        }
    }
}
