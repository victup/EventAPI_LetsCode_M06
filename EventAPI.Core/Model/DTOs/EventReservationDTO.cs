using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Model.DTOs
{
    public class EventReservationDTO
    {
        public long IdReservation { get; }
        public long IdEvent { get; set; }
        public string PersonName { get; set; }
        public long Quantity { get; set; }

        public EventReservationDTO(long idReservation, long idEvent, string personName, long quantity)
        {
            IdReservation = idReservation;
            IdEvent = idEvent;
            PersonName = personName;
            Quantity = quantity;
        }
    }
}
