using Dapper;
using EventAPI.Core.Interfaces.RepositorysInterface;
using EventAPI.Core.Model;
using EventAPI.Core.Model.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAPI.Infra.Data.Repositorys
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private readonly IConfiguration _configuration;

        public EventReservationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool AddNewBooking(EventReservation newBooking)
        {
            var query = $"INSERT INTO EventReservation VALUES (@IdEvent, @PersonName, @Quantity)";

            var parameters = new DynamicParameters();

            parameters.Add("IdEvent", newBooking.IdEvent);
            parameters.Add("PersonName", newBooking.PersonName);
            parameters.Add("Quantity", newBooking.Quantity);
      

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public List<EventReservation> GetBookingByPersonNameAndTitle(string personName, string eventTitle)
        {
            var query = @$"SELECT R.IdReservation, R.IdEvent, R.PersonName, R.Quantity FROM CityEvent AS E INNER JOIN EventReservation R 
ON E.IdEvent = R.IdEvent WHERE R.PersonName = @PersonName AND E.Title LIKE '%'+@EventTitle+'%'";

            var parameters = new DynamicParameters();
            parameters.Add("PersonName", personName);
            parameters.Add("EventTitle", eventTitle);


            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<EventReservation>(query, parameters).ToList();
        }

        public long GetIdBooking(string personName, string eventTitle)
        {
            var queryIdBooking = @$"SELECT R.IdReservation FROM EventReservation AS R
INNER JOIN CityEvent AS E
ON R.IdEvent = E.IdEvent
WHERE R.PersonName = @PersonName AND E.Title = @EventTitle";

            var parameters = new DynamicParameters();
            parameters.Add("PersonName", personName);
            parameters.Add("EventTitle", eventTitle);


            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return long.Parse(conn.QueryFirstOrDefault<long>(queryIdBooking, parameters).ToString());
        }

        public long GetIdBooking(long idBooking)
        {
            var queryIdBooking = @$"SELECT R.IdReservation FROM EventReservation AS R WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters();
            parameters.Add("IdReservation", idBooking);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return long.Parse(conn.QueryFirstOrDefault<long>(queryIdBooking, parameters).ToString());
        }

        public bool RemoveBooking(long idEventReservation)
        {

            var query = $@"DELETE FROM EventReservation WHERE idReservation = @idEventReservation";

            var parameters = new DynamicParameters();
            parameters.Add("idEventReservation", idEventReservation);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                return conn.Execute(query, parameters) == 1;
            }
            catch (Exception)
            {
                Console.WriteLine("Não foi possível excluir o evento.");
                Console.WriteLine("Local Error: CityEventRepository/RemoveEvent");
                return false;
            }

        }


        public bool UpdateBooking(long idReservation, long quantity)
        {
            var query = "UPDATE EventReservation SET Quantity = @Quantity WHERE IdReservation = @IdReservation";

            var parameters = new DynamicParameters();

            parameters.Add("IdReservation", idReservation);
            parameters.Add("Quantity", quantity);
           

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }
    }
}
