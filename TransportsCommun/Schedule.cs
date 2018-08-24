using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportsCommun
{
    class Schedule
    {
        public string stopId { get; set; }
        public List<int> trips { get; set; }
        public string stopName { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string parentStation { get; set; }
    }

    public class Arret
    {
        public string stopId { get; set; }
        public List<int> trips { get; set; }
        public string stopName { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string parentStation { get; set; }
    }

    public class PrevNext
    {
        public List<Arret> arrets { get; set; }
        public long prevTime { get; set; }
        public long nextTime { get; set; }
    }

}
