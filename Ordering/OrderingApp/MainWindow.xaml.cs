using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace OrderingApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public User(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void btn_register_Click(object sender, RoutedEventArgs e)
        {
                var user = new JavaScriptSerializer().Serialize(new User(txt_name.Text, txt_email.Text));

            HttpResponseMessage response = null;
            try
            {
                using (var client = new HttpClient())
                {
                    response = client.PostAsync(
                     "http://127.0.0.1:5000/User/CreateUser",
                      new StringContent(user, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("OK");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ERROR");
            }
        }

        public void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
