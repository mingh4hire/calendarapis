using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace calendar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //(localdb)\ProjectsV13 (DESKTOP-SK5QC59\jerem)
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IEnumerable<dynamic> GetCalendar()
        {
            //(localdb)\ProjectsV13
            ///(localdb)\ProjectsV13 (DESKTOP-SK5QC59\jerem)
            ///Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;
            //Server = (localdb)\ProjectsV13 (DESKTOP-SK5QC59\jerem); Database = master;  

            using (var connection = new SqlConnection(@"Server = (localdb)\ProjectsV13; Database = master;"))
            {
                SqlCommand command = new SqlCommand("select * from calendarEvent where creator = @creator", connection);
                command.Parameters.AddWithValue("@creator", "dan");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                var events = new List<dynamic>();
                try
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}, {1}",
                        reader["startDate"], reader["endDate"]));// etc
                        events.Add(new
                        {
                            startDate = reader["startDate"],
                            endDate = reader["endDate"],
                            eventTitle = reader["eventTitle"],
                            eventDescription = reader["eventDescription"],
                            eventType = reader["eventType"]
                        });
                    }
                    return events;
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
        }

        [HttpPost]
        public IActionResult Post()
        {
            using (var connection = new SqlConnection(@"Server = (localdb)\ProjectsV13; Database = master;"))
            {
                SqlCommand command = new SqlCommand(@"insert into calendarEvent (eventTitle, eventDescription, startDate, endDate, creator ) values
                    (@eventTitle, @eventDescription, @startDate, @endDate, @creator)", connection);
                command.Parameters.AddWithValue("@creator", "dan");
                command.Parameters.AddWithValue("@endDate", new DateTime(2021, 9,5));
                command.Parameters.AddWithValue("@startDate", new DateTime(2021,9,12));
                command.Parameters.AddWithValue("@eventDescription", "rent a movie");
                command.Parameters.AddWithValue("@eventTitle", " over 19 dollar once");
                command.Parameters.AddWithValue("@eventType", "unimportant");
                connection.Open();
                    command.ExecuteNonQuery();
                return StatusCode(201);
            }
        }


        [HttpGet("GetWeather")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
