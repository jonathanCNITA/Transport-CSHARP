using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using MetroApi;
using System.Linq;

namespace TransportsCommun
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Common transport project");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            //-- Position du campus: y:45.18549, x:5.72792
            MetroReq LinesReq = new MetroReq("5.72792", "45.18549", "500");
            List<StationModel> stations = JsonConvert.DeserializeObject<List<StationModel>>(LinesReq.GetResponseAsString());

            //-- Log initial returned stations
            foreach(StationModel station in stations )
            {
                Console.WriteLine(station.name);
            }

            Console.WriteLine("<----------------------------------> CLEAN DATAS");
            
            //-- Create A dictionary with each stop and their lines
            Dictionary<string, List<string>> stationsDict = new Dictionary<string, List<string>>();
            stationsDict = ToolBox.GetListNameWithoutDuplicateAsDictionnary(stations);

            //-- Log stations cleaned
            foreach (KeyValuePair<string, List<string>> station in stationsDict)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(station.Key);
                foreach(string line in station.Value)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(line);
                }
            }

            Console.ReadLine();
        }
    }
}
