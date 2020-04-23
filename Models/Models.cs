using MappingTheMBTA.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MappingTheMBTA.Models
{
    public class Dataset
    {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public int Id { get; set; }

        public int EffectiveDate { get; set; } // in unix days
        public List<Trip> Trips { get; set; }

        public Dataset Clone() => (Dataset)MemberwiseClone();
    }

    public class Trip
    {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public int Id { get; set; }

        public string TripID { get; set; }
        public string Line { get; set; }

        public List<Stop> Stops { get; set; }

        public long StartTime { get; set; } // in unix seconds
        public long EndTime { get; set; } // in unix seconds

        public int DirectionID { get; set; } // the direction the trip is facing (inbound / outbound)
        public string Destination { get; set; }
    }

    public class Stop
    {
        [Key]
        [System.Text.Json.Serialization.JsonIgnore]
        public int Id { get; set; }

        public string PlaceID { get; set; } // front end format (i.e "place-<station code>")

        public long Arrival { get; set; } // time in unix, scheduled time then arrived time
        public long Departure { get; set; } // time in unix, scheduled time then arrived time
        public long? Delta { get; set; } // filled in after a train arrives
    }

    // at startup, fetches the list of routes & their termini from the mbta
    public static class Route
    {
        public static Dictionary<string, string[]> Routes = new Dictionary<string, string[]>();

        public static async Task Populate()
        {
            // 0 = light rail
            // 1 = subway
            int[] types = new int[] { 0, 1 };

            List<Tuple<Task<string>, int>> pending = new List<Tuple<Task<string>, int>>();

            // add each type to queue
            foreach (int type in types)
                pending.Add(Tuple.Create(new MBTAWeb().FetchJSONAsync(MBTAWeb.Endpoint.routes, $"?filter[type]={type}"), type));

            // process the queue
            foreach (var item in pending)
                ProcessLine(await item.Item1);
        }

        private static void ProcessLine(string jsonLine)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(jsonLine).data;
            foreach (var route in data)
            {
                string id = route.id;

                // ignore Mattapan
                if (id != "Mattapan")
                {
                    string[] dirs = route.attributes.direction_destinations.ToObject<string[]>();
                    Routes.Add(id, dirs);
                }
            }
        }
    }
}