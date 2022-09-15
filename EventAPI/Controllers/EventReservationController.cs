﻿using EventAPI.Core.Interfaces.ServicesInterface;
using EventAPI.Core.Model;
using EventAPI.Core.Model.DTOs;
using EventAPI.Core.Services;
using EventAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [TypeFilter(typeof(LogResourceFilter))]
    [TypeFilter(typeof(LogAuthorizationFilter))]
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
            Console.WriteLine($"Buscando as reservas de eventos com titulo {title} para a pessoa  {person}");

            return Ok(_reservationService.GetBookingByPersonNameAndTitle(person, title));
        }


        [HttpPost("/comrpar_ingressos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EventReservation> NewEvent([FromBody] EventReservation newBooking)
        {

            Console.WriteLine($"Reservando evento {newBooking.IdEvent} para pessoa {newBooking.PersonName}");

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
            Console.WriteLine($"Alterando quantidade de reservas do evento {idReservation} para {quantity}");

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
