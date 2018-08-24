using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsCommun
{
    class ToolBox
    {
        public static Dictionary<string, List<string>> GetListNameWithoutDuplicateAsDictionnary(List<Station> stationList)
        {
            Dictionary<string, List<string>> stationsDict = new Dictionary<string, List<string>>();
            foreach (Station station in stationList)
            {
                if (!stationsDict.ContainsKey(station.name))
                {
                    stationsDict.Add(station.name, station.lines);
                }
                else
                {
                    stationsDict[station.name] = (stationsDict[station.name].Concat(station.lines)).Distinct().ToList();
                }
            }
            return stationsDict;
        }

        public static string ConvertMetroSecondToReadableTime(int totalSecs)
        {
            int hours = totalSecs / 3600;
            int minutes = (totalSecs % 3600) / 60;
            int seconds = totalSecs % 60;
            return "next: " + hours + " : " + minutes;
        }
        
    }
}
