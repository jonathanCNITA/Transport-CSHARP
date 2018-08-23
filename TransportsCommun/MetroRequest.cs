using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TransportsCommun
{
    class MetroRequest
    {
        string UrlApi { get; set; }

        public MetroRequest()
        {
            UrlApi = "http://data.metromobilite.fr/api/linesNear/json?x=5.72792&y=45.18549&dist=800&details=true";
        }

        public MetroRequest(string apiRoute)
        {
            UrlApi = apiRoute;
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
