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
            MetroRequest LinesReq = new MetroRequest();
            List<Station> stations = JsonConvert.DeserializeObject<List<Station>>(LinesReq.GetResponseAsString());

            //-- Log initial returned stations
            foreach( Station station in stations )
            {
                Console.WriteLine(station.name);
            }

            Console.WriteLine("----------------------------------");
            Dictionary<string, List<string>> stationsDict = new Dictionary<string, List<string>>();
            foreach (Station station in stations)
            {
                if (!stationsDict.ContainsKey(station.name))
                {
                    stationsDict.Add(station.name, station.lines);
                }
                else
                {
                    stationsDict[station.name] = (stationsDict[station.name].Concat(station.lines).ToList()).Distinct().ToList();
                }
            }

            //-- Log stations cleaned
            foreach (KeyValuePair<string, List<string>> station in stationsDict)
            {
                Console.WriteLine(station.Key);
                foreach(string line in station.Value)
                {
                    Console.WriteLine(line);
                }
            }
            

            Console.ReadLine();
        }
    }
}
