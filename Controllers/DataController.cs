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
        public Dataset GetActual(int effective)
        {
            Console.WriteLine($"{DateTime.Now} | REQ ./api/actual");
            if (effective == DateTime.Now.ConvertToEffective())
                return Predicted.Include();
            else
                return Database.Retrieve(effective);
        }

        [HttpGet("dates")]
        public List<int> GetDates()
        {
            Console.WriteLine($"{DateTime.Now} | REQ ./api/dates");
            return Database.GetDates();
        }
    }
}
