using EventAPI.Core.Interfaces.RepositorysInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace EventAPI.Filters
{
    public class DeleteBookingActionFilter : ActionFilterAttribute
    {
        public IEventReservationRepository _eventReservationRepository;
        public DeleteBookingActionFilter(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string personName = (string)context.ActionArguments["personName"];
            string eventTitle = (string)context.ActionArguments["eventTitle"];

            if(personName != null && eventTitle != null)
            {
                if (_eventReservationRepository.GetIdBooking(personName, eventTitle) == 0)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
                    Console.WriteLine("Filtro DeleteBookingActionFilter: Não existe a reserva com os parâmetros informados.");
                }
                else
                    Console.WriteLine("Filtro DeleteBookingActionFilter: Reserva existe.");

            }


        }


    }
}
