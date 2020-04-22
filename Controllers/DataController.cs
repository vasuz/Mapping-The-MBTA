using System.Collections.Generic;
using MappingTheMBTA.Models;
using MappingTheMBTA.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MappingTheMBTA.Controllers
{
    [ApiController]
    [Route("api")]
    public class DataController : ControllerBase
    {
        [HttpGet("data")]
        // takes in unix day
        public Dataset GetActual(int date)
        {
            Console.WriteLine($"{DateTime.Now} | REQ | GET /api/data?date={date}");
            if (date == DateTime.Now.ConvertToEffective())
                return Sources.Today;
            else
                return Database.Retrieve(date);
        }

        [HttpGet("dates")]
        public List<int> GetDates()
        {
            Console.WriteLine($"{DateTime.Now} | REQ | GET /api/dates");
            return Database.GetDates();
        }
    }
}
