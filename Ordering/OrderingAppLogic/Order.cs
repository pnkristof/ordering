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
    public class Order
    {

        public static IEnumerable<Product> GetCatalog(ProductSet categories)
        {
            var Categories = new JavaScriptSerializer().Serialize(categories);

            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    response = client.PostAsync(
                     "http://127.0.0.1:5000/Order/Catalog",
                      new StringContent(Categories, Encoding.UTF8, "application/json")).Result;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Product>>(json);
                }

            }
            catch (Exception e)
            {

            }
            return new List<Product>();
        }

    }
}
