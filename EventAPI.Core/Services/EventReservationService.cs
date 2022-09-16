using EventAPI.Core.Interfaces.MapperInterface;
using EventAPI.Core.Interfaces.RepositorysInterface;
using EventAPI.Core.Interfaces.ServicesInterface;
using EventAPI.Core.Model;
using EventAPI.Core.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Services
{
    public class EventReservationService : IEventReservationService
    {
        public IEventReservationRepository _reservationRepository;
        public ICityEventRepository _cityEventRepository;
        public IBookingMapper _bookingMapper;
        public EventReservationService(IEventReservationRepository reservationRepository, ICityEventRepository cityEventRepository, IBookingMapper bookingMapper)
        {
            _reservationRepository = reservationRepository;
            _cityEventRepository = cityEventRepository;
            _bookingMapper = bookingMapper; 
        }
        public bool AddNewBooking(long idEvent, AddNewBookingRequestDTO bookingDto)
        {

            var bookingDtoToBooking = _bookingMapper.NewBookingDtoToEventReservation(idEvent, bookingDto);

            return _reservationRepository.AddNewBooking(bookingDtoToBooking);
        }

        public List<BookingByPersonAndTitleDTO> GetBookingByPersonNameAndTitle(string personName, string eventTitle)
        {
            var eventList = _cityEventRepository.GetEventByPersonNameAndTitle(personName, eventTitle);

            var bookingList = _reservationRepository.GetBookingByPersonNameAndTitle(personName, eventTitle);

            return _bookingMapper.EventAnEventReservationToBookingDTO(eventList, bookingList);
           
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
