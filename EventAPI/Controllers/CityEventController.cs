using EventAPI.Core.Interfaces.ServicesInterface;
using EventAPI.Core.Model;
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
    public class CityEventController : Controller
    {
        public ICityEventService _cityEventService;

        public CityEventController(ICityEventService cityEventService)
        {
            _cityEventService=cityEventService;
        }

        [HttpGet("/pesquisar_eventos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "cliente, admin")]
        public ActionResult<List<Event>> SearchEvents(string titleEvent)
        {
            Console.WriteLine($"Iniciando busca do evento através do title fornecido. Titulo: {titleEvent}");

            return Ok(_cityEventService.GetEventByTitle(titleEvent));
        }

        [HttpGet("/eventos_por_local_e_data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "cliente, admin")]
        public ActionResult<List<Event>> SearchEventsByLocalAndDate(string local, DateTime data)
        {
            Console.WriteLine($"Iniciando busca do evento através do local e data fornecidos. Local: {local} / Data:{data}");

            return Ok(_cityEventService.GetEventByLocalAndDate(local, data));
        }

        [HttpGet("/eventos_por_preco_e_data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "cliente, admin")]
        public ActionResult<List<Event>> SearchEventsByPriceAndData(decimal minValue, decimal maxValue, DateTime data)
        {
            Console.WriteLine($"Iniciando busca do evento através do preços e data fornecidos. valor mínimo R$ {minValue}/ Valor máximo: R${maxValue}/ Data: {data}");

            return Ok(_cityEventService.GetEventByPriceAndDate(minValue, maxValue, data));
        }

        [HttpPost("/inserir_evento")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "admin")]
        public ActionResult<Event> NewEvent([FromBody] Event newEvent)
        {
            Console.WriteLine($"Criando novo Evento. Nome: {newEvent.Title}");

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ServiceFilter(typeof(ValidateCityEventActionFilter))]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateEvent(string titleEvent, Event eventForUpdate)
        {
            Console.WriteLine($"Atualizando o evento  {titleEvent} p/ novo Titulo: {eventForUpdate.Title}");

            if (!_cityEventService.UpdateEvent(titleEvent, eventForUpdate))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok(eventForUpdate);
        }


        [HttpDelete("/apagar_evento")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ServiceFilter(typeof(ValidateCityEventActionFilter))]
        [Authorize(Roles = "admin")]
        public ActionResult<List<Event>> CancelEvent(string titleEvent)
        {
            Console.WriteLine($"Cancelando Evento. Title: {titleEvent}");

            if (!_cityEventService.RemoveEvent(titleEvent))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

    }
}
