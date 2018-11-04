using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace OrderingAppLogic
{
    

    public class Security
    {
        public static User CurrentUser;
        public static bool loggedIn = false;


        public static bool Register(string name, string email)
        {
            var user = new JavaScriptSerializer().Serialize(new User(name, email));

            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    response = client.PostAsync(
                     Config.host + Config.port + "/User/CreateUser",
                      new StringContent(user, Encoding.UTF8, "application/json")).Result;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return true;
                }

            }
            catch (Exception e)
            {

            }
            return false;
        }

        public static bool Login(string email, string password)
        {
            var DTO =  new { Email = email, Password = password };
            var user = new JavaScriptSerializer().Serialize(DTO);

            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    response = client.PostAsync(
                     Config.host + Config.port + "/Security/Login",
                      new StringContent(user, Encoding.UTF8, "application/json")).Result;
                }
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    loggedIn = true;
                    return true;
                }

            }
            catch (Exception e)
            {

            }
            return false;
        }
    }


}
