using EventAPI.Core.Model;
using EventAPI.Core.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Interfaces.ServicesInterface
{
    public interface ICityEventService
    {
        public bool AddNewEvent(EventDTO newEvent);
        public bool UpdateEvent(long idEvent, EventDTO eventForUpdate);
        public bool RemoveEvent(string titleEvent);
        public EventDTO GetEventByTitle(string titleEvent);
        public EventDTO GetEventByLocalAndDate(string localEvent, DateTime dateEvent);
        public EventDTO GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime dateEvent);

    }
}
