using EventAPI.Core.Model;
using EventAPI.Core.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Interfaces.MapperInterface
{
    public interface IBookingMapper
    {
        List<BookingByPersonAndTitleDTO> EventAnEventReservationToBookingDTO(List<Event> listEvent, List<EventReservation> listBooking);

        EventReservation NewBookingDtoToEventReservation(long idEvent, AddNewBookingRequestDTO booking);
    }
}
