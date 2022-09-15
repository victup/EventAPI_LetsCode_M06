using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Model
{
    public class EventReservation
    {
        public long IdReservation { get; }

        [Required(ErrorMessage = "O ID do evento é um campo com preenchimento obrigatório. Refere-se ao ID de um evento já cadastrado.")]
        public long IdEvent { get; set; }

        [Required(ErrorMessage = "Nome da pessoa é um campo com preenchimento obrigatório")]
        [StringLength(maximumLength: 200, ErrorMessage = "O campo PersonName não pode conter mais de 200 caracteres)")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "É necessário informar a quantidade de ingressos para o evento no campo Quantity")]
        public long Quantity { get; set; }

        public EventReservation(long idReservation, long idEvent, string personName, long quantity)
        {
            IdReservation = idReservation;
            IdEvent = idEvent;
            PersonName = personName;
            Quantity = quantity;
        }
    }
}
