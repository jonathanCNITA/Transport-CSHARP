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
            foreach( Station station in stations )
            {
                Console.WriteLine(station.name);
            }

            Console.WriteLine("----------------------------------");

            // Tricks founded on stackOverFlow : https://stackoverflow.com/questions/15829309/remove-item-have-same-key-in-list-c-sharp
            List<Station> stationsFiltered = stations.GroupBy(t => t.name).Select(g => g.First()).ToList();
            foreach (Station station in stationsFiltered)
            {
                Console.WriteLine(station.name);
            }
           
            Console.ReadLine();
        }
    }
}
