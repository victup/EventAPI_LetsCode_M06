using EventAPI.Core.Interfaces.ServicesInterface;
using EventAPI.Core.Model;
using EventAPI.Core.Model.DTOs;
using EventAPI.Core.Services;
using EventAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")] //define que a entrada é em json
    [Produces("application/json")] //define que a saida é em json
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [TypeFilter(typeof(LogResourceFilter))]
    [TypeFilter(typeof(LogAuthorizationFilter))]
    [EnableCors("PolicyCors")]
    [Authorize]
    public class EventReservationController : Controller
    {

        public IEventReservationService _reservationService;

        public EventReservationController(IEventReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("/consultar_reserva_por_pessoa_e_evento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "cliente, admin")]
        public ActionResult<List<BookingByPersonAndTitleDTO>> SearchReservationByPersonAndTitle(string person, string title)
        {
            Console.WriteLine($"Buscando as reservas de eventos com titulo {title} para a pessoa  {person}");

            return Ok(_reservationService.GetBookingByPersonNameAndTitle(person, title));
        }


        [HttpPost("/criar_reserva")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "cliente, admin")]
        public ActionResult<EventReservation> NewEvent(long idEvent, AddNewBookingRequestDTO newBooking)
        {

            Console.WriteLine($"Reservando evento {idEvent} para pessoa {newBooking.PersonName}");

            if (!_reservationService.AddNewBooking(idEvent, newBooking))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(NewEvent), newBooking);
        }

        [HttpPut("/alterar_reserva")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ServiceFilter(typeof(UpdateBookingActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateBooking(long idReservation, long quantity)
        {
            Console.WriteLine($"Alterando quantidade de reservas do evento {idReservation} para {quantity}");

            if (!_reservationService.UpdateBooking(idReservation, quantity))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpDelete("/apagar_reserva")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ServiceFilter(typeof(DeleteBookingActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult CancelBooking(string personName, string eventTitle)
        {
            Console.WriteLine($"Cancelando o evento {eventTitle} d@ cliente {personName}");

            Console.WriteLine("Iniciando");
            if (!_reservationService.RemoveBooking(personName, eventTitle))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

    }
}
