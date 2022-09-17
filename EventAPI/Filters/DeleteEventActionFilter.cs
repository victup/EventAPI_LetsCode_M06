using EventAPI.Core.Interfaces.RepositorysInterface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Filters
{
    public class DeleteEventActionFilter : ActionFilterAttribute
    {


        public ICityEventRepository _cityEventRepository;
        public DeleteEventActionFilter(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string titleEvent = (string)context.ActionArguments["titleEvent"];


            if (titleEvent != null)
            {
                if (_cityEventRepository.GetIdEvent(titleEvent) == 0)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
                    Console.WriteLine("Filtro ValidateCityEventActionFilter: Não existe um evento com o titulo informado.");
                }
                else
                    Console.WriteLine("Filtro DeleteBookingActionFilter: Evento encontrado.");

            }
        }
    }
}