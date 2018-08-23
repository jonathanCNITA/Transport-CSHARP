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

            //-- Position du campus: y:45.18549, x:5.72792
            string urlApi = "http://data.metromobilite.fr/api/linesNear/json?x=5.72792&y=45.18549&dist=800&details=true";

            WebRequest request = WebRequest.Create(urlApi);
            WebResponse response = request.GetResponse();
            Console.WriteLine("Reponse status : " + ((HttpWebResponse)response).StatusDescription);

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            
            Console.WriteLine("------------------------------");
            Console.WriteLine(responseFromServer);
            reader.Close();
            response.Close();

            Console.ReadLine();
        }
    }
}
