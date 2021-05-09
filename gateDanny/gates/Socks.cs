using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gateBeta.gates
{
    class Socks
    {

        public string proxy(){

            try
            {
                var client = new RestClient("https://olympusgenerador.tech/guardar/ajax/generador.php?op=proxy");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                return response.Content.Trim();
            }
            catch (Exception )
            {
                return "0";
            }
        }

        public string proxyUp(string ip)
        {

            try
            {
                var client = new RestClient("https://olympusgenerador.tech/guardar/ajax/generador.php?op=proxyUp&ip="+ip);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                return response.Content.Trim();
            }
            catch (Exception)
            {
                return "0";
            }
        }

        
    }
    
}
