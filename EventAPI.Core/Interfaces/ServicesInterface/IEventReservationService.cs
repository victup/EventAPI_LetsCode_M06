using EventAPI.Core.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Interfaces.ServicesInterface
{
    public interface IEventReservationService
    {
        public bool AddNewBooking(EventReservationDTO newBooking);
        public bool UpdateBooking(EventReservationDTO bookingForUpdate);
        public bool RemoveBooking(long IdReservation);
        public EventReservationDTO GetBookingByPersonNameAndTitle(string personName, string eventTitle);
    }
}
