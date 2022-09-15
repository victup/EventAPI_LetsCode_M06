using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Model
{
    public class Event
    {

        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Título é um campo com preenchimento obrigatório")]
        [StringLength(maximumLength:100, ErrorMessage = "O campo titulo não pode conter mais de 100 caracteres)")]
        public string Title { get; set; }

        
        [StringLength(maximumLength: 255, ErrorMessage = "O campo descrição não pode conter mais de 255 caracteres)")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A Data do evento é um campo com preenchimento obrigatório")]
        public DateTime DateHourEvent { get; set; }

        [Required(ErrorMessage = "Local é um campo com preenchimento obrigatório")]
        [StringLength(maximumLength: 150, ErrorMessage = "O campo Local não pode conter mais de 150 caracteres)")]
        public string Local { get; set; }

        [StringLength(maximumLength: 255, ErrorMessage = "O campo Endereço não pode conter mais de 255 caracteres)")]
        public string Address { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }

        public Event(long idEvent, string title, string description, DateTime dateHourEvent, string local, string address, decimal price, bool status)
        {
            IdEvent = idEvent;
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
