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

namespace OrderingApp
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        public void btn_SignIn_Click(object sender, RoutedEventArgs e)
        {
            grid_signup.Visibility = Visibility.Hidden;
            grid_signin.Visibility = Visibility.Visible;
            
        }

        public void btn_SignUp_Click(object sender, RoutedEventArgs e)
        {
            grid_signin.Visibility = Visibility.Hidden;
            grid_signup.Visibility = Visibility.Visible;
        }

        public void btn_register_Click(object sender, RoutedEventArgs e)
        {
            if (OrderingAppLogic.Security.Register(txt_SigUpName.Text, txt_SignUpEmail.Text))
            {
                MainWindow.Result.Content = "Registered";
                if(OrderingAppLogic.Security.Login(txt_SignUpEmail.Text, txt_SignUpPw.Text))
                    MainWindow.Result.Content += ", Logged in successfully";
            }
            else
                MainWindow.Result.Content = "Something went wrong, registration failed";
        }

        public void btn_Authenticate_Click(object sender, RoutedEventArgs e)
        {
            if (OrderingAppLogic.Security.Login(txt_SigInEmail.Text, txt_SignInPw.Text))
                MainWindow.Result.Content = "Logged in successfully";
            else
                MainWindow.Result.Content = "Something went wrong, sign in failed";
        }
    }
}
