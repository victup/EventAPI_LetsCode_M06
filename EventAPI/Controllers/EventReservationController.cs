using EventAPI.Core.Interfaces.ServicesInterface;
using EventAPI.Core.Model;
using EventAPI.Core.Model.DTOs;
using EventAPI.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventReservationController : Controller
    {

        public IEventReservationService _reservationService;

        public EventReservationController(IEventReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("/reserva_por_pessoa_e_evento")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<GetBookingByPersonAndTitleResponseDTO>> SearchReservationByPersonAndTitle(string person, string title)
        {
            return Ok(_reservationService.GetBookingByPersonNameAndTitle(person, title));
        }


        [HttpPost("/comrpar_ingressos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EventReservation> NewEvent([FromBody] EventReservation newBooking)
        {
            Console.WriteLine("Iniciando");
            if (!_reservationService.AddNewBooking(newBooking))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(NewEvent), newBooking);
        }

        [HttpPut("/alterar_reserva")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateBooking(long idReservation, long quantity)
        {
            Console.WriteLine("Iniciando");
            if (!_reservationService.UpdateBooking(idReservation, quantity))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpDelete("/apagar_resserva")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CancelBooking(string personName, string eventTitle)
        {
            Console.WriteLine("Iniciando");
            if (!_reservationService.RemoveBooking(personName, eventTitle))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

    }
}
