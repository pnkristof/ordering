using OrderingApp;
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

    public partial class MainWindow : Window
    {
        static Home home = new Home();
        static Menu menu = new Menu();

        public static Label Result = new Label
        {
            Content = "App Started",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 10,
            Margin = new Thickness(0, 0, 0, 0)
        };
        

        public MainWindow()
        {
            InitializeComponent();
            grd_Status.Children.Add(Result);
            Result.Content = "Main window initialized";
            

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {
                throw new InvalidOperationException();
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Result.Content = "Shutting down";
            Application.Current.Shutdown();
        }

        public void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            //MoveCursorMenu(index);

            switch (index)
            {
                case 0:
                    if (!Security.loggedIn)
                        MainFrame.Content = home;
                    else
                        MainFrame.Content = new HomeLoggedIn();
                    MainWindow.Result.Content = "Clicked on Home";
                    break;
                case 1:
                    MainFrame.Content = menu;
                    MainWindow.Result.Content = "Clicked on Menu";
                    break;
                case 2:
                    MainFrame.Content = new CartTotal();
                    MainWindow.Result.Content = "Clicked on Cart";
                    break;
            }
        }

        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
