using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrderingAppLogic
{
    public class Config
    {

        public static string host = "http://localhost";
        public static string port = ":5000";


        public static async Task<bool> Ping()
        {
            

            HttpResponseMessage response = null;
            
                using (var client = new HttpClient())
                {
                    response = client.PostAsync(
                     Config.host + Config.port + "/Ping",
                      new StringContent("ping", Encoding.UTF8, "application/json")).Result;
                }
            return response.StatusCode == System.Net.HttpStatusCode.OK;
            
        }
    }
}
