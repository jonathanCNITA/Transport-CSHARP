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

            void getSchedules()
            {
                //-- Log stations cleaned with schedules
                long TimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds() + (3600000);
                foreach (KeyValuePair<string, List<string>> station in stationsDict)
                {
                    Console.WriteLine(station.Key);
                    foreach (string line in station.Value)
                    {
                        Console.WriteLine(line);
                        MetroRequest NextSchedule = new MetroRequest("http://data.metromobilite.fr/api/ficheHoraires/json?route=" + line + "&time=" + TimeStamp);
                        JObject ApiTime = JObject.Parse(NextSchedule.GetResponseAsString());
                        List<JToken> listeArrets = ApiTime["0"]["arrets"].Children().ToList();
                        JToken nextTimestamp = ApiTime["0"]["nextTime"];
                        List<Arret> AllArrets = new List<Arret>();

                        Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                        foreach (JToken arr in listeArrets)
                        {
                            // Clean "|" datas
                            for (int i = 0; i < arr["trips"].Count(); i++)
                            {
                                Console.WriteLine(arr["trips"][i]);
                                if ( arr["trips"][i].GetType() != typeof(int))
                                {
                                    arr["trips"][i].Remove();
                                }
                            }
                            // Console.WriteLine(arr);
                            
                            // Console.WriteLine("------------------");
                        }

                        Console.WriteLine("=============================================");
                        /*
                        foreach (JToken arr in listeArrets)
                        {
                            Console.WriteLine(arr);
                            // Arret arretToAdd = arr.ToObject<Arret>();
                            // AllArrets.Add(arretToAdd);
                        }
                        */
                        /*
                        foreach (Arret arret in AllArrets)
                        {
                            // Console.WriteLine("Arret.stopName : " + arret.stopName + "  station.Key : " + station.Key);
                            if(arret.stopName == station.Key)
                            {
                                foreach (int time in arret.trips)
                                {
                                    Console.WriteLine(time);
                                    Console.WriteLine(ToolBox.ConvertMetroSecondToReadableTime(time));
                                }
                            }
                        }
                        */
                    }
                }
            }

            getSchedules();

            Console.WriteLine("----------------------------------");
            /*
            long TimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds() + (3600000);

            Boolean getNewDatas = true;
            MetroRequest NextSchedule = new MetroRequest("http://data.metromobilite.fr/api/ficheHoraires/json?route=SEM:B&time=" + TimeStamp);
            while (getNewDatas)
            {
                Console.WriteLine("Set a request with this timestamp : " + TimeStamp);
                JObject ApiTime = JObject.Parse(NextSchedule.GetResponseAsString());
                List<JToken> listeArrets = ApiTime["0"]["arrets"].Children().ToList();
                JToken nextTimestamp = ApiTime["0"]["nextTime"];
                Console.WriteLine("Next Time is : " + nextTimestamp);

                List<Arret> AllArrets = new List<Arret>();
                foreach( JToken arr in listeArrets)
                {
                    Arret arretToAdd = arr.ToObject<Arret>();
                    AllArrets.Add(arretToAdd);
                }

                foreach( Arret arret in AllArrets)
                {
                    Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                    Console.WriteLine(arret.stopName);
                    foreach( int time in arret.trips)
                    {
                        Console.WriteLine(time);
                        Console.WriteLine(ToolBox.ConvertMetroSecondToReadableTime(time));
                    }
                }

                Console.WriteLine("Check next schedules : Y or N");
                string res = Console.ReadLine();
                getNewDatas = (res == "Y");

                Console.WriteLine("Next Time is : " + ApiTime["0"]["nextTime"]);
                TimeStamp = (long)ApiTime["0"]["nextTime"];
                NextSchedule = new MetroRequest("http://data.metromobilite.fr/api/ficheHoraires/json?route=SEM:B&time=" + TimeStamp);
            }
            */
            Console.WriteLine("----------------------------------");
 
            Console.WriteLine(ToolBox.ConvertMetroSecondToReadableTime(42900));

            Console.ReadLine();
        }
    }
}
