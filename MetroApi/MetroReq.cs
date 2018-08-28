using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace MetroApi
{
    public class MetroReq
    {
        string UrlApi { get; set; }

        public MetroReq()
        {
            UrlApi = "http://data.metromobilite.fr/api/linesNear/json?x=5.72792&y=45.18549&dist=500&details=true";
        }

        public MetroReq(string apiRoute)
        {
            UrlApi = apiRoute;
        }

        public MetroReq(string posX, string posY, string dist)
        {
            UrlApi = "http://data.metromobilite.fr/api/linesNear/json?x=" + posX + "&y=" + posY + "&dist=" + dist + "&details=true";
        }

        public string GetResponseAsString()
        {
            WebRequest request = WebRequest.Create(UrlApi);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return responseFromServer;
        }
    }
}
