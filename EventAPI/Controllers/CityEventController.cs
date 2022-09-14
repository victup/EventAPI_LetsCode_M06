using EventAPI.Core.Interfaces.ServicesInterface;
using EventAPI.Core.Model;
using EventAPI.Core.Model.DTOs;
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

        [HttpPost("/inserir_evento")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[TypeFilter(typeof(LogActionFilter))]
        public ActionResult<EventDTO> NewEvent([FromBody] EventDTO newEvent)
        {
            Console.WriteLine("Iniciando");
            if (!_cityEventService.AddNewEvent(newEvent))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(NewEvent), newEvent);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateEvent(long id, EventDTO eventForUpdate)
        {
            Console.WriteLine("Iniciando");
            if (!_cityEventService.UpdateEvent(id, eventForUpdate))
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok(eventForUpdate);
        }


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<List<EventDTO>> CancelEvent(string titleEvent)
        {
            Console.WriteLine("Iniciando");
            if (!_cityEventService.RemoveEvent(titleEvent))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            return Ok();
        }

    }
}
