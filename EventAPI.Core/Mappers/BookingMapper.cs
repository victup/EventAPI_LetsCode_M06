using EventAPI.Core.Interfaces.MapperInterface;
using EventAPI.Core.Model;
using EventAPI.Core.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Mappers
{
    public class BookingMapper : IBookingMapper
    {
        public List<BookingByPersonAndTitleDTO> EventAnEventReservationToBookingDTO(List<Event> listEvent, List<EventReservation> listBooking)
        {

            List<BookingByPersonAndTitleDTO> bookingDtoResponse = new();

            for (int i = 0; i < listEvent.Count; i++)
            {
                if (listBooking[i].IdEvent == listEvent[i].IdEvent)
                {
                    bookingDtoResponse.Add(new BookingByPersonAndTitleDTO(
                    listBooking[i].PersonName,
                    listBooking[i].Quantity,
                    listEvent[i].Title,
                    listEvent[i].Description,
                    listEvent[i].DateHourEvent,
                    listEvent[i].Local,
                    listEvent[i].Address,
                    listEvent[i].Price,
                    listEvent[i].Status
                    ));
                }
            }

            return bookingDtoResponse;
        }

        public EventReservation NewBookingDtoToEventReservation(long idEvent, AddNewBookingRequestDTO bookingDTO)
        {

            EventReservation bookingDtoToEventReservation = new EventReservation
                (
                    0,
                    idEvent, 
                    bookingDTO.PersonName, 
                    bookingDTO.Quantity
                );

            return bookingDtoToEventReservation;
        }
    }
}
