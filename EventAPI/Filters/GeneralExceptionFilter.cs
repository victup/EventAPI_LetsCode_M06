using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;

namespace EventAPI.Filters
{
    public class GeneralExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails
            {
                Status = 0,
                Title = "Erro inesperado",
                Detail = "Erro inesperado ao se comunicar com o banco de dados",
                Type = context.Exception.GetType().Name
            };

            Console.WriteLine($"Tipo da exceção {context.Exception.GetType().Name}, mensagem {context.Exception.Message}, stack trace {context.Exception.StackTrace}");

            switch (context.Exception)
            { 
                case SqlException:
                    problem.Status = StatusCodes.Status503ServiceUnavailable;
                    problem.Detail = "Erro inesperado ao se comunicar com o banco de dados";
                    context.Result = new ObjectResult(problem);
                    break;
                case NullReferenceException:
                    problem.Status = StatusCodes.Status417ExpectationFailed;
                    problem.Detail = "Erro inesperado no sistema";
                    context.Result = new ObjectResult(problem);
                    break;
                default:
                    problem.Status = StatusCodes.Status500InternalServerError;
                    problem.Detail = "Erro inesperado. Tente novamente";
                    context.Result = new ObjectResult(problem);
                    break;
            }
        }
    }
}
