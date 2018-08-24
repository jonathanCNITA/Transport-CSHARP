using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace TransportsCommun
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Common transport project");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //-- Position du campus: y:45.18549, x:5.72792
            MetroRequest LinesReq = new MetroRequest("5.72792", "45.18549", "500");
            List<Station> stations = JsonConvert.DeserializeObject<List<Station>>(LinesReq.GetResponseAsString());

            //-- Log initial returned stations
            foreach( Station station in stations )
            {
                Console.WriteLine(station.name);
            }

            Console.WriteLine("----------------------------------");
            
            //-- Create A dictionary with each stop and their lines
            Dictionary<string, List<string>> stationsDict = new Dictionary<string, List<string>>();
            stationsDict = ToolBox.GetListNameWithoutDuplicateAsDictionnary(stations);

            //-- Log stations cleaned
            foreach (KeyValuePair<string, List<string>> station in stationsDict)
            {
                Console.WriteLine(station.Key);
                foreach(string line in station.Value)
                {
                    Console.WriteLine(line);
                }
            }


            Console.WriteLine("----------------------------------");

            long TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            Console.WriteLine(TimeStamp);
            MetroRequest NextSchedule = new MetroRequest("http://data.metromobilite.fr/api/ficheHoraires/json?route=SEM:B&time=" + TimeStamp);
            Console.WriteLine(NextSchedule.GetResponseAsString());
            // List<Schedule> schedules = JsonConvert.DeserializeObject<List<Schedule>>(NextSchedule.GetResponseAsString());
            Console.WriteLine(NextSchedule.GetResponseAsString());


            Console.WriteLine("----------------------------------");

            Console.WriteLine(ToolBox.ConvertMetroSecondToReadableTime(42900));

            Console.ReadLine();
        }
    }
}
