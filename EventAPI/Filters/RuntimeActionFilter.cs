using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace EventAPI.Filters
{
    public class RuntimeActionFilter : IActionFilter
    {

        Stopwatch stopwatch = new();

        public void OnActionExecuting(ActionExecutingContext context)
        {
            stopwatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"Tempo de execução: {stopwatch.Elapsed.TotalSeconds} segundos");

            stopwatch.Stop();
        }
    }
}
