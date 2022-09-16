using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Core.Model.DTOs
{
    public class AddNewBookingRequestDTO
    {

        [Required(ErrorMessage = "Nome da pessoa é um campo com preenchimento obrigatório")]
        [StringLength(maximumLength: 200, ErrorMessage = "O campo PersonName não pode conter mais de 200 caracteres)")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "É necessário informar a quantidade de ingressos para o evento no campo Quantity")]
        public long Quantity { get; set; }

        public AddNewBookingRequestDTO(string personName, long quantity)
        {
            PersonName = personName;
            Quantity = quantity;
        }
    }
}
