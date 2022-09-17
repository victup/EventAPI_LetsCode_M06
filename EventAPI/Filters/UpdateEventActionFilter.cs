using EventAPI.Core.Interfaces.RepositorysInterface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Filters
{
    public class UpdateEventActionFilter : ActionFilterAttribute
    {
        public ICityEventRepository _cityEventRepository;
        public UpdateEventActionFilter(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idEvent = (long)context.ActionArguments["idEvent"];


            if (idEvent > 0)
            {
                if (_cityEventRepository.GetIdEvent(idEvent) == 0)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
                    Console.WriteLine("Filtro UpdateEventActionFilter: Não existe um evento com o titulo informado.");
                }
                else
                    Console.WriteLine("Filtro UpdateEventActionFilter: Evento encontrado.");

            }
        }
    }
}
