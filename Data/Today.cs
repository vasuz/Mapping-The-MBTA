using MappingTheMBTA.Models;
using System;
using System.Threading.Tasks;

namespace MappingTheMBTA.Data
{
    public static partial class Sources
    {
        public static Dataset Today;

        public static async Task Populate()
        {
            // Attempt to load existing data
            Dataset loaded = Database.Retrieve(DateTime.Now.ConvertToEffective());
            if (loaded.Trips != null)
            {
                // Successfully loaded existing data from db
                Console.WriteLine($"{DateTime.Now} | DB | Found existing dataset #{loaded.EffectiveDate}");
                Today = loaded;
            }
            else
            {
                // Populate the schedule for the first time, wait for it then save
                Console.WriteLine($"{DateTime.Now} | DB | Existing data not found, fetching new data");
                await Scheduled.Update();
                Database.Capture();
            }
        }
    }
}
