using EventAPI.Core.Model;
using EventAPI.Core.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Interfaces.RepositorysInterface
{
    public interface IEventReservationRepository
    {
        public bool AddNewBooking(EventReservation newBooking);
        public bool UpdateBooking(long idReservation, long quantity);
        public bool RemoveBooking(long idBookingReservation);
        public List<EventReservation> GetBookingByPersonNameAndTitle(string personName, string eventTitle);
        public long GetIdBooking(string personName, string eventTitle);
        public long GetIdBooking(long idBooking);


    }
}
