using Dapper;
using EventAPI.Core.Interfaces.RepositorysInterface;
using EventAPI.Core.Model;
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
        public bool AddNewEvent(Event newEvent)
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

        public List<Event> GetEventByLocalAndDate(string localEvent, DateTime dateEvent)
        {

           var dateHourEvent = dateEvent.ToString("yyyy-MM-ddTHH:mm:ss.fff");

            var query = $"SELECT * FROM CityEvent AS E WHERE E.Local = @Local AND CAST(E.DateHourEvent AS DATE) =  CAST(@Date AS Date)";

            var parameters = new DynamicParameters();
            parameters.Add("Local", localEvent);
            parameters.Add("Date", dateHourEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Event>(query, parameters).ToList();
        }

        public List<Event> GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime dateEvent)
        {
            var dateHourEvent = dateEvent.ToString("yyyy-MM-ddTHH:mm:ss.fff");

            var query = $"SELECT * FROM CityEvent AS E WHERE E.Price BETWEEN @MinPrice AND @MaxPrice AND CAST(E.DateHourEvent AS DATE) = CAST(@Date AS Date)";

            var parameters = new DynamicParameters();
            parameters.Add("MinPrice", minPrice);
            parameters.Add("MaxPrice", maxPrice);
            parameters.Add ("Date", dateHourEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Event>(query, parameters).ToList();
        }

        public List<Event> GetEventByTitle(string titleEvent)
        {

            var query = $"SELECT * FROM CityEvent WHERE Title like '%'+@Title+'%'";

            var parameters = new DynamicParameters();
            parameters.Add("Title", titleEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            return conn.Query<Event>(query, parameters).ToList();
        }

    

    public bool RemoveEvent(string titleEvent)
    {
        var query = "DELETE FROM CityEvent where Title = @Title";

        var parameters = new DynamicParameters();
        parameters.Add("Title", titleEvent);

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

    public bool UpdateEvent(long idEvent, Event eventForUpdate)
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
