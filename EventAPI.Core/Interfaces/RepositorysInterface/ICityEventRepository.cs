using EventAPI.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Interfaces.RepositorysInterface
{
    public interface ICityEventRepository
    {
        public bool AddNewEvent(Event newEvent);
        public bool UpdateEvent(long idEvent, Event eventForUpdate);
        public bool RemoveEvent(string titleEvent);
        public List<Event> GetEventByTitle(string titleEvent);
        public List<Event> GetEventByLocalAndDate(string localEvent, DateTime dateEvent);
        public List<Event> GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime dateEvent);
    }
}
