using EventAPI.Core.Interfaces.RepositorysInterface;
using EventAPI.Core.Interfaces.ServicesInterface;
using EventAPI.Core.Model;
using EventAPI.Core.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _reservationRepository;
        public EventReservationService(IEventReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public bool AddNewBooking(EventReservation newBooking)
        {
            return _reservationRepository.AddNewBooking(newBooking);
        }

        public List<GetBookingByPersonAndTitleResponseDTO> GetBookingByPersonNameAndTitle(string personName, string eventTitle)
        {
            return _reservationRepository.GetBookingByPersonNameAndTitle(personName, eventTitle); 
        }

        public bool RemoveBooking(string personName, string eventTitle)
        {
           return (_reservationRepository.RemoveBooking(personName, eventTitle));
        }

        public bool UpdateBooking(long idReservation, long quantity)
        {
            return _reservationRepository.UpdateBooking(idReservation, quantity);
        }
    }
}
