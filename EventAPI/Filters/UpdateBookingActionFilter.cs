using EventAPI.Core.Interfaces.RepositorysInterface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Filters
{
    public class UpdateBookingActionFilter : ActionFilterAttribute
    {

        public IEventReservationRepository _eventReservationRepository;
        public UpdateBookingActionFilter(IEventReservationRepository eventReservationRepository)
        {
            _eventReservationRepository = eventReservationRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            long idReservation = (long)context.ActionArguments["idReservation"];

            if (idReservation !=  0)
            {
                if (_eventReservationRepository.GetIdBooking(idReservation) == 0)
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
                    Console.WriteLine("Filtro UpdateBookingActionFilter: Não existe a reserva com os parâmetros informados.");
                }
                else
                    Console.WriteLine("Filtro DeleteBookingActionFilter: Reserva Existe.");

            }


        }

    }
}
