using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroApi
{
    /// <summary>
    /// My tool box with various methods to help the treatment of json Datas
    /// </summary>
    public class ToolBox
    {
        /// <summary>
        /// GetListNameWithoutDuplicateAsDictionnary
        /// </summary>
        /// <param name="stationList"> my parameter</param>
        /// <returns>a dictionary without "doublon" for station names and lines</returns>
        public static Dictionary<string, List<string>> GetListNameWithoutDuplicateAsDictionnary(List<StationModel> stationList)
        {
            Dictionary<string, List<string>> stationsDict = new Dictionary<string, List<string>>();
            foreach (StationModel station in stationList)
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

        /// <summary>
        /// ConvertMetroSecondToReadableTime
        /// </summary>
        /// <param name="totalSecs"> my parameter</param>
        /// <returns>Return a readable string from the time in second since midnight like the f****** metro api</returns>
        public static string ConvertMetroSecondToReadableTime(int totalSecs)
        {
            int hours = totalSecs / 3600;
            int minutes = (totalSecs % 3600) / 60;
            int seconds = totalSecs % 60;
            return "next: " + hours + " : " + minutes;
        }
    }
}
