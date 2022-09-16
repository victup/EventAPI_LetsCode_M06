using EventAPI.Core.Model;
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
        public bool AddNewBooking(long idEvent, AddNewBookingRequestDTO newBooking);
        public bool UpdateBooking(long idReservation, long quantity);
        public bool RemoveBooking(string personName, string eventTitle);
        public List<BookingByPersonAndTitleDTO> GetBookingByPersonNameAndTitle(string personName, string eventTitle);
    }
}
