using EventAPI.Core.Interfaces.RepositorysInterface;
using EventAPI.Core.Interfaces.ServicesInterface;
using EventAPI.Core.Model.DTOs;
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

        public bool AddNewEvent(EventDTO newEvent)
        {
            return _cityEventRepository.AddNewEvent(newEvent);
        }

        public EventDTO GetEventByLocalAndDate(string localEvent, DateTime dateEvent)
        {
            throw new NotImplementedException();
        }

        public EventDTO GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime dateEvent)
        {
            throw new NotImplementedException();
        }

        public EventDTO GetEventByTitle(string titleEvent)
        {
            throw new NotImplementedException();
        }

        public bool RemoveEvent(string titleEvent)
        {
            if (!_cityEventRepository.CheckExistenceOfActiveReservations(titleEvent))
                return _cityEventRepository.RemoveEvent(titleEvent);
            else
            return false;
        }

        public bool UpdateEvent(long idEvent, EventDTO eventForUpdate)
        {
            return _cityEventRepository.UpdateEvent(idEvent, eventForUpdate);
        }
    }
}
