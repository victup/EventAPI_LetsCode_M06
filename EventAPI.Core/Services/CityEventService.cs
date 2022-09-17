using EventAPI.Core.Interfaces.RepositorysInterface;
using EventAPI.Core.Interfaces.ServicesInterface;
using EventAPI.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Services
{
    public class CityEventService: ICityEventService
    {
        public ICityEventRepository _cityEventRepository;
        public CityEventService(ICityEventRepository cityEventRepository)
        {
            _cityEventRepository = cityEventRepository;
        }

        public bool AddNewEvent(Event newEvent)
        {
            return _cityEventRepository.AddNewEvent(newEvent);
        }

        public List<Event> GetEventByLocalAndDate(string localEvent, DateTime dateEvent)
        {
            return _cityEventRepository.GetEventByLocalAndDate(localEvent, dateEvent);
        }

        public List<Event> GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime dateEvent)
        {
            return _cityEventRepository.GetEventByPriceAndDate(minPrice, maxPrice, dateEvent);
        }

        public List<Event> GetEventByTitle(string titleEvent)
        {
            return _cityEventRepository.GetEventByTitle(titleEvent);
        }

        public bool RemoveEvent(string titleEvent)
        {
            var eventForRemove = _cityEventRepository.GetIdEvent(titleEvent);
            var reservationEvent = _cityEventRepository.CheckExistenceOfReservation(eventForRemove);

            if (reservationEvent == 0)
                 return _cityEventRepository.RemoveEvent(eventForRemove);

            else
                return _cityEventRepository.InactivateEvent(eventForRemove);
        }

        public bool UpdateEvent(long idEvent, Event eventForUpdate)
        {
            var idEventForUpdate = _cityEventRepository.GetIdEvent(idEvent);
            
            if(idEventForUpdate > 0)
                return _cityEventRepository.UpdateEvent(idEvent, eventForUpdate);
            else return false;
            
        }
    }
}
