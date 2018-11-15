using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace OrderingAppLogic
{
    public class Address
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string DeliverTo { get; set; }
        public string Phone { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string TheRest { get; set; }
        
        public static bool AddAddress(Address address)
        {
            var body = new JavaScriptSerializer().Serialize(address);

            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    response = client.PostAsync(
                     Config.host + Config.port + "/User/AddAddress",
                      new StringContent(body, Encoding.UTF8, "application/json")).Result;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return true;
                }

            }
            catch (Exception e)
            {

            }
            return false;
        }

        public static IEnumerable<Address> GetAddresses(int userId)
        {
            var body = new
            {
                UserId = userId
            };
            var UserId = new JavaScriptSerializer().Serialize(body);

            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    response = client.PostAsync(
                     Config.host + Config.port + "/User/GetAddresses",
                      new StringContent(UserId, Encoding.UTF8, "application/json")).Result;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Address>>(json);
                }

            }
            catch (Exception e)
            {

            }
            return new List<Address>();
        }
    }
}
