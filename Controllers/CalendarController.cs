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
    public class CalendarController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CalendarController> _logger;

        public CalendarController(ILogger<CalendarController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Calendar")]
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
        [HttpGet("GetCal")]
        public IEnumerable<int> GetNumbers()
        {
            return new int[] { 3, 4, 56 };
        }

        [HttpGet("GetEvents")]
        public IEnumerable<dynamic> GetCalendar(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "dan";
            }
            //(localdb)\ProjectsV13
            ///(localdb)\ProjectsV13 (DESKTOP-SK5QC59\jerem)
            ///Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;
            //Server = (localdb)\ProjectsV13 (DESKTOP-SK5QC59\jerem); Database = master;  

            using (var connection = new SqlConnection(@"Server = (localdb)\ProjectsV13; Database = master;"))
            {
                SqlCommand command = new SqlCommand("select * from calendarEvent c left join eventType e on c.eventtype = e.name  where c.creator = @creator", connection);
                command.Parameters.AddWithValue("@creator", name);
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
                            eventType = reader["eventType"],
                            color = reader["color"]
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



            //using (var conn = new SqlConnection(@"(localdb)\ProjectsV13 (DESKTOP-SK5QC59\jerem)"))
            //{
            //    conn.Query("select * from calendarEvent").ToList();
            //}
        }
    }
}
