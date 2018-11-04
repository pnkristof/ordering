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
    /// Interaction logic for SubMenu.xaml
    /// </summary>
    public partial class SubMenu : Page
    {
        static List<ButtonProductPair> buttons = new List<ButtonProductPair>();

        public SubMenu()
        {
            InitializeComponent();
        }

        public SubMenu(List<Product> products)
        {
            InitializeComponent();
            Grid grid = new Grid();
            int left_margin = 80;
            int top_margin = 0;



            foreach (var product in products)
            {

                //-------- Name Label
                Label labelName = new Label();
                labelName.Content = product.Name;
                labelName.Margin = new Thickness(left_margin, top_margin + 100, 0, 0);
                labelName.HorizontalAlignment = HorizontalAlignment.Left;
                labelName.HorizontalContentAlignment = HorizontalAlignment.Center;
                labelName.VerticalAlignment = VerticalAlignment.Top;

                //-------- Price Label
                Label labelPrice = new Label();
                labelPrice.Content = product.price;
                labelPrice.Margin = new Thickness(left_margin + 70, top_margin + 127, 0, 0);
                labelPrice.HorizontalAlignment = HorizontalAlignment.Left;
                labelPrice.VerticalAlignment = VerticalAlignment.Top;

                //-------- Button
                Button button = new Button();
                button.Content = " To Cart ";
                button.Margin = new Thickness(left_margin, top_margin + 130, 0, 0);
                button.HorizontalAlignment = HorizontalAlignment.Left;
                button.VerticalAlignment = VerticalAlignment.Top;
                button.Width = 55;
                button.Click += AddToCart;
                buttons.Add(new ButtonProductPair(button, product));

                //-------- Image
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(product.ImgUrl));
                image.Margin = new Thickness(left_margin + 10, top_margin, 0, 0);
                image.HorizontalAlignment = HorizontalAlignment.Left;
                image.VerticalAlignment = VerticalAlignment.Top;
                if (image.Source.Width < image.Source.Height)
                {
                    double ratio = image.Source.Width / image.Source.Height;
                    image.Height = 100;
                    image.Width = (int)(ratio * 100);
                }
                else
                {
                    double ratio =  image.Source.Height / image.Source.Width;
                    image.Width = 100;
                    image.Height = (int)(ratio * 100);
                }

                if(image.Height < 100)
                {
                    image.Margin = new Thickness(left_margin + 10, top_margin + (100 - image.Height), 0, 0);
                }


                //-------- Add to Grid
                grd_SubMenu.Children.Add(labelName);
                grd_SubMenu.Children.Add(labelPrice);
                grd_SubMenu.Children.Add(button);
                grd_SubMenu.Children.Add(image);

                //-------- Set margins
                if (left_margin != 530)
                {
                    left_margin += 150;
                }
                else
                {
                    left_margin = 80;
                    top_margin += 170;
                }

            }


        }

        public void AddToCart(object sender, RoutedEventArgs e)
        {
            Button thisButton = (Button)sender;

            Product thisProduct = (from x in buttons
                                  where x.button == thisButton
                                  select x.product).First();

            Cart.Add(thisProduct);
            MainWindow.Result.Content = 
                thisProduct.Name 
                + " (" + thisProduct.price + ") "
                + "added to cart"
                + " (" + Cart.showTotal + ") ";
        }
    }
}
