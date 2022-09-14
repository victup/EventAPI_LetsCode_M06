using Dapper;
using EventAPI.Core.Interfaces.RepositorysInterface;
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
    public class CityEventRepository : ICityEventRepository
    {
        private readonly IConfiguration _configuration;


        public CityEventRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool AddNewEvent(EventDTO newEvent)
        {
            // por seguranca nao se coloca nos values produto.Nome…. usa-se o @

            var query = $"INSERT INTO CityEvent VALUES(@Title, @Description, @DateHourEvent, @Local, @Address, @Price, @Status)";

            var parameters = new DynamicParameters();

            parameters.Add("Title", newEvent.Title);
            parameters.Add("Description", newEvent.Description);
            parameters.Add("DateHourEvent", newEvent.DateHourEvent);
            parameters.Add("Local", newEvent.Local);
            parameters.Add("Address", newEvent.Address);
            parameters.Add("Price", newEvent.Price);
            parameters.Add("Status", newEvent.Status);

            //abrir conexao

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1; //== linhas afetadas
        }

        public bool CheckExistenceOfActiveReservations(string title)
        {
            var query = "SELECT E.Title, E.Description, E.DateHourEvent, E.Local, R.PersonName, R.Quantity FROM CityEvent AS E INNER JOIN EventReservation AS R ON R.IdEvent = E.IdEvent WHERE E.Title like '%@Title%';";

            var parameters = new DynamicParameters();
            parameters.Add("Title", title);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            if (conn.Execute(query, parameters) == 1)
                return true;
            else
            return false;
        }

        public EventDTO GetEventByLocalAndDate(string localEvent, DateTime dateEvent)
        {
            throw new NotImplementedException();
        }

        public EventDTO GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime dateEvent)
        {
            throw new NotImplementedException();
        }

        public EventDTO GetEventByTitle(string titleEvent)
        {
            throw new NotImplementedException();
        }

        public bool RemoveEvent(string titleEvent)
        {
            var query = "DELETE FROM CityEvent where Title = @Title";

            var parameters = new DynamicParameters();
            parameters.Add("Title", titleEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Execute(query, parameters) == 1;
        }

        public bool UpdateEvent(long idEvent, EventDTO eventForUpdate)
        { 
                var query = "UPDATE CityEvent SET Title = @Title, Description = @Description, DateHourEvent = @DateHourEvent, Local = @Local, Address = @Address, Price = @Price, Status = @Status WHERE IdEvent = @IdEvent";

                var parameters = new DynamicParameters();

                parameters.Add("IdEvent", idEvent);
                parameters.Add("Title", eventForUpdate.Title);
                parameters.Add("Description", eventForUpdate.Description);
                parameters.Add("DateHourEvent", eventForUpdate.DateHourEvent);
                parameters.Add("Local", eventForUpdate.Local);
                parameters.Add("Address", eventForUpdate.Address);
                parameters.Add("Price", eventForUpdate.Price);
                parameters.Add("Status", eventForUpdate.Status);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                return conn.Execute(query, parameters) == 1;
            
        }
    }
}
