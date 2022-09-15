using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Model.DTOs
{
    public class GetBookingByPersonAndTitleResponseDTO
    {
        public string PersonName { get; set; }
        public long Quantity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateHourEvent { get; set; }
        public string Local { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }

        public GetBookingByPersonAndTitleResponseDTO(string personName, long quantity, string title, string description, DateTime dateHourEvent, string local, string address, decimal price, bool status)
        {
            PersonName = personName;
            Quantity = quantity;
            Title = title;
            Description = description;
            DateHourEvent = dateHourEvent;
            Local = local;
            Address = address;
            Price = price;
            Status = status;
        }
    }
}
