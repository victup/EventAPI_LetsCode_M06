using EventAPI.Core.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Interfaces.RepositorysInterface
{
    public interface ICityEventRepository
    {
        public bool AddNewEvent(EventDTO newEvent);
        public bool UpdateEvent(long idEvent, EventDTO eventForUpdate);
        public bool RemoveEvent(string titleEvent);
        public bool CheckExistenceOfActiveReservations(string title);
        public EventDTO GetEventByTitle(string titleEvent);
        public EventDTO GetEventByLocalAndDate(string localEvent, DateTime dateEvent);
        public EventDTO GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime dateEvent);
    }
}
