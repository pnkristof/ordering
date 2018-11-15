using OrderingAppLogic;
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
    /// Interaction logic for HomeLoggedIn.xaml
    /// </summary>
    public partial class HomeLoggedIn : Page
    {
        public HomeLoggedIn()
        {
            InitializeComponent();
            lbl_welcome.Content = "Hello, " + Security.CurrentUser.Name + "!";
            ShowAddresses();
        }

        public void ShowAddresses()
        {
            List<Address> addresses = Address.GetAddresses(Security.CurrentUser.Id).ToList();
            stc_addresses.Children.Clear();
            

            foreach (var a in addresses)
            {
                Grid grd = new Grid();

                Label deliverTo = new Label();
                deliverTo.Content = a.DeliverTo + "\n" + a.Phone;
                deliverTo.Margin = new Thickness(30, 0, 0, 0);
                deliverTo.FontSize = 14;
                deliverTo.VerticalAlignment = VerticalAlignment.Center;

                Label address = new Label();
                address.Content = a.Zip + "\n" + a.City + "\n" + a.TheRest;
                address.Margin = new Thickness(322, 0, 0, 0);
                address.FontSize = 14;
                address.VerticalAlignment = VerticalAlignment.Center;

                Rectangle r = new Rectangle();
                r.Width = 670;
                r.Height = 1;
                r.Fill = new SolidColorBrush(Colors.Black);
                r.HorizontalAlignment = HorizontalAlignment.Center;
                r.VerticalAlignment = VerticalAlignment.Bottom;

                grd.Children.Add(deliverTo);
                grd.Children.Add(address);

                stc_addresses.Children.Add(grd);
                stc_addresses.Children.Add(r);
            }

        }

        public void btn_addAddress_Click(object sender, RoutedEventArgs e)
        {
            scr_addresses.Visibility = Visibility.Hidden;
            grd_addAddress.Visibility = Visibility.Visible;
            btn_addAddress.Visibility = Visibility.Hidden;
            grd_top.Visibility = Visibility.Hidden;
        }

        public void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (Address.AddAddress(new Address
            {
                CustomerId = Security.CurrentUser.Id,
                DeliverTo = txt_deliverTo.Text,
                Phone = txt_phone.Text,
                Zip = txt_zip.Text,
                City = txt_city.Text,
                TheRest = txt_address.Text
            }))
            {
                MainWindow.Result.Content = "Address Successfully added";
                ShowAddresses();
                btn_cancel_Click(sender, e);
            }
            else
            {
                MainWindow.Result.Content = "Failed to add address";
            }
        }

        public void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            grd_addAddress.Visibility = Visibility.Hidden;
            scr_addresses.Visibility = Visibility.Visible;
            btn_addAddress.Visibility = Visibility.Visible;
            grd_top.Visibility = Visibility.Visible;
            txt_address.Text = "";
            txt_city.Text = "";
            txt_deliverTo.Text = "";
            txt_phone.Text = "";
            txt_zip.Text = "";
        }
    }
}
