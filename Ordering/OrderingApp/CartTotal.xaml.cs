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
    /// Interaction logic for CartTotal.xaml
    /// </summary>
    public partial class CartTotal : Page
    {

        public static List<ButtonProductPair> productInCart = new List<ButtonProductPair>();

        public void TotalizeCart()
        {
            stc_products.Children.Clear();

            if (Cart.Set.Count != 0)
            {
                grd_top.Visibility = Visibility.Visible;
            }
            else
            {
                lbl_EmptyCart.Visibility = Visibility.Visible;
            }

            foreach (var item in Cart.Set)
            {
                Grid grd = new Grid();

                Label name = new Label();
                name.Margin = new Thickness(30, 0, 0, 0);
                name.Content = item.product.Name;
                name.FontSize = 14;

                Label quantity = new Label();
                quantity.Margin = new Thickness(180, 0, 0, 0);
                quantity.Content = item.quantity;
                quantity.FontSize = 14;

                Button decraseQuantity = new Button();
                decraseQuantity.Margin = new Thickness(155, 0, 0, 0);
                decraseQuantity.Content = "<";
                decraseQuantity.Click += DecraseQuantity;
                decraseQuantity.Width = 15;
                decraseQuantity.HorizontalAlignment = HorizontalAlignment.Left;

                productInCart.Add(new ButtonProductPair(decraseQuantity, item.product));

                Button incraseQuantity = new Button();
                incraseQuantity.Margin = new Thickness(210, 0, 0, 0);
                incraseQuantity.Content = ">";
                incraseQuantity.Click += IncraseQuantity;
                incraseQuantity.Width = 15;
                incraseQuantity.HorizontalAlignment = HorizontalAlignment.Left;

                productInCart.Add(new ButtonProductPair(incraseQuantity, item.product));

                Label productPrice = new Label();
                productPrice.Margin = new Thickness(280, 0, 0, 0);
                productPrice.Content = item.product.price;
                productPrice.FontSize = 14;

                Label totalProdctPrice = new Label();
                totalProdctPrice.Margin = new Thickness(380, 0, 0, 0);
                totalProdctPrice.Content = "$" + ((item.product.Price * item.quantity) / 100).ToString() + "." + ((item.product.Price * item.quantity) % 100).ToString();
                totalProdctPrice.FontSize = 14;

                Rectangle r = new Rectangle();
                r.Width = 500;
                r.Height = 1;
                r.Fill = new SolidColorBrush(Colors.Black);
                r.HorizontalAlignment = HorizontalAlignment.Center;
                r.VerticalAlignment = VerticalAlignment.Bottom;

                grd.Children.Add(name);
                grd.Children.Add(decraseQuantity);
                grd.Children.Add(quantity);
                grd.Children.Add(incraseQuantity);
                grd.Children.Add(productPrice);
                grd.Children.Add(totalProdctPrice);

                stc_products.Children.Add(grd);
                stc_products.Children.Add(r);
            }

            int totalPrice = 0;
            int prodQuantity = 0;

            foreach (var item in Cart.Set)
            {
                totalPrice += item.product.Price * item.quantity;
                prodQuantity += item.quantity;

                lbl_prodQuantity.Content = prodQuantity.ToString() + " product(s)";
                lbl_totalPrice.Content = (totalPrice / 100).ToString() + "." + (totalPrice % 100).ToString();
            }
        }

        public CartTotal()
        {
            InitializeComponent();
            TotalizeCart();
        }
        public void DecraseQuantity(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            var p = (from x in productInCart
                    where x.button == button
                    select x.product).First();

            var q = (from x in Cart.Set
                     where x.product.Id == p.Id
                     select x).First().quantity;

            if (q > 1)
                Cart.Set.Where(x => x.product.Id == p.Id).First().quantity--;
            else
                Cart.Set.Remove(Cart.Set.Where(x => x.product.Id == p.Id).First());


            TotalizeCart();
        }

        public void IncraseQuantity(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            var p = (from x in productInCart
                     where x.button == button
                     select x.product).First();

            var q = (from x in Cart.Set
                     where x.product.Id == p.Id
                     select x).First().quantity;

            
            Cart.Set.Where(x => x.product.Id == p.Id).First().quantity++;


            TotalizeCart();
        }



    }

}
