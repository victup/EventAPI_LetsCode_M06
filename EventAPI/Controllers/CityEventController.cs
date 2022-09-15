using EventAPI.Core.Interfaces.ServicesInterface;
using EventAPI.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityEventController : Controller
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService=cityEventService;
        }

        [HttpGet("/pesquisar_eventos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Event>> SearchEvents(string title)
        {
            return Ok(_cityEventService.GetEventByTitle(title));
        }

        [HttpGet("/eventos_por_local_e_data")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Event>> SearchEventsByLocalAndDate(string local, DateTime data)
        {
            return Ok(_cityEventService.GetEventByLocalAndDate(local, data));
        }

        [HttpGet("/eventos_por_preco_e_data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Event>> SearchEventsByPriceAndData(decimal minValue, decimal maxValue, DateTime data)
        {
            return Ok(_cityEventService.GetEventByPriceAndDate(minValue, maxValue, data));
        }

        [HttpPost("/inserir_evento")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Event> NewEvent([FromBody] Event newEvent)
        {
            Console.WriteLine("Iniciando");
            if (!_cityEventService.AddNewEvent(newEvent))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(NewEvent), newEvent);
        }

        [HttpPut("/alterar_evento")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEvent(long id, Event eventForUpdate)
        {
            Console.WriteLine("Iniciando");
            if (!_cityEventService.UpdateEvent(id, eventForUpdate))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok(eventForUpdate);
        }


        [HttpDelete("/apagar_evento")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Event>> CancelEvent(string titleEvent)
        {
            Console.WriteLine("Iniciando");
            if (!_cityEventService.RemoveEvent(titleEvent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

    }
}
