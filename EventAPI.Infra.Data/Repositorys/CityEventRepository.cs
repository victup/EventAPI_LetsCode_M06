using Dapper;
using EventAPI.Core.Interfaces.RepositorysInterface;
using EventAPI.Core.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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

            try
            {
                return conn.Execute(query, parameters) == 1; //== linhas afetadas
            }
            catch(Exception)
            {
                Console.WriteLine("Erro no banco de Dados: CityEventRepository/AddNewEvent ");
                throw;
            }
          
           
        }

        public List<Event> GetEventByLocalAndDate(string localEvent, DateTime dateEvent)
        {

           var dateHourEvent = dateEvent.ToString("yyyy-MM-ddTHH:mm:ss.fff");

            var query = $"SELECT * FROM CityEvent AS E WHERE E.Local = @Local AND CAST(E.DateHourEvent AS DATE) =  CAST(@Date AS Date) AND Status = 1";

            var parameters = new DynamicParameters();
            parameters.Add("Local", localEvent);
            parameters.Add("Date", dateHourEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                return conn.Query<Event>(query, parameters).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Erro no banco de Dados: CityEventRepository/GetEventByLocalAndDate ");
                throw;
            }
            
        }

        public List<Event> GetEventByPriceAndDate(decimal minPrice, decimal maxPrice, DateTime dateEvent)
        {
            var dateHourEvent = dateEvent.ToString("yyyy-MM-ddTHH:mm:ss.fff");

            var query = $"SELECT * FROM CityEvent AS E WHERE E.Price BETWEEN @MinPrice AND @MaxPrice AND CAST(E.DateHourEvent AS DATE) = CAST(@Date AS Date) AND Status = 1";

            var parameters = new DynamicParameters();
            parameters.Add("MinPrice", minPrice);
            parameters.Add("MaxPrice", maxPrice);
            parameters.Add ("Date", dateHourEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                return conn.Query<Event>(query, parameters).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Erro no banco de Dados: CityEventRepository/GetEventByPriceAndDate ");
                throw;
            }
            
        }

        public List<Event> GetEventByTitle(string titleEvent)
        {

            var query = $"SELECT * FROM CityEvent WHERE Title like '%'+@Title+'%' AND Status = 1";

            var parameters = new DynamicParameters();
            parameters.Add("Title", titleEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            try
            {
                return conn.Query<Event>(query, parameters).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Erro no banco de Dados: CityEventRepository/GetEventByTitle ");
                throw;
            }
            
        }

        public List<Event> GetEventByPersonNameAndTitle(string personName, string titleEvent)
        {

            var query = @$"SELECT E.IdEvent, E.Title, E.Description, E.DateHourEvent, E.Local, E.Address, E.Price, E.Status FROM CityEvent AS E INNER JOIN EventReservation R 
ON E.IdEvent = R.IdEvent WHERE R.PersonName = @PersonName AND E.Title LIKE '%'+@Title+'%';";

            var parameters = new DynamicParameters();
            parameters.Add("PersonName", personName);
            parameters.Add("Title", titleEvent);
    
            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            

            try
            {
                return conn.Query<Event>(query, parameters).ToList();
            }
            catch (Exception)
            {
                Console.WriteLine("Erro no banco de Dados: CityEventRepository/GetEventByPersonNameAndTitle ");
                throw;
            }
        }

        public long GetIdEvent(string titleEvent)
        {
           
                var query = $"SELECT idEvent FROM CityEvent WHERE Title like '%'+@Title+'%'";

                var parameters = new DynamicParameters();
                parameters.Add("Title", titleEvent);

                using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

                

                try
                {
                    return long.Parse(conn.QueryFirstOrDefault<long>(query, parameters).ToString());
                }
                catch (Exception)
                {
                    Console.WriteLine("Erro no banco de Dados: CityEventRepository/GetIdEvent(title)");
                    throw;
                }
        }
        public long GetIdEvent(long idEvent)
        {

            var query = $"SELECT idEvent FROM CityEvent WHERE idEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            

            try
            {
                return long.Parse(conn.QueryFirstOrDefault<long>(query, parameters).ToString());
            }
            catch (Exception)
            {
                Console.WriteLine("Erro no banco de Dados: CityEventRepository/GetIdEvent(idEvent) ");
                throw;
            }

        }

        public bool RemoveEvent(long idEvent)
        {
            var queryDeletar = "DELETE FROM CityEvent where idEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));


            try
            {
                return conn.Execute(queryDeletar, parameters) == 1;
            }
            catch (Exception)
            {
                Console.WriteLine("Erro no banco de Dados: CityEventRepository/RemoveEvent ");
                throw;
            }
             
        }

        public long CheckExistenceOfReservation (long idEvent)
        {
            var query = @$"SELECT R.IdReservation FROM CityEvent AS E INNER JOIN EventReservation R 
ON E.IdEvent = R.IdEvent WHERE E.IdEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            

            try
            {
                return long.Parse(conn.QueryFirstOrDefault<long>(query, parameters).ToString());
            }
            catch (Exception)
            {
                Console.WriteLine("Erro no banco de Dados: CityEventRepository/CheckExistenceOfReservation ");
                throw;
            }
        }

        public bool InactivateEvent(long idEvent)
        {
            var query = @$"UPDATE CityEvent SET Status = 0 WHERE idEvent = @IdEvent";

            var parameters = new DynamicParameters();
            parameters.Add("IdEvent", idEvent);

            using var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

           

            try
            {
                return conn.Execute(query, parameters) == 1;
            }
            catch (Exception)
            {
                Console.WriteLine("Erro no banco de Dados: CityEventRepository/InactivateEvent ");
                throw;
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

            

            try
            {
                return conn.Execute(query, parameters) == 1;
            }
            catch (Exception)
            {
                Console.WriteLine("Erro no banco de Dados: CityEventRepository/UpdateEvent ");
                throw;
            }

        } 
    }
} 
