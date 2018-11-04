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
    /// Interaction logic for Manu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        static int subMenuIndex = 0;
        static List<SubMenu> subMenus = new List<SubMenu>();
        public Menu()
        {
            InitializeComponent();
        }

        public void btn_filter_Click(object sender, RoutedEventArgs e)
        {
            subMenus.Clear();
            List<Product> products = Order.GetCatalog(GetPreferences()).ToList();
            MainWindow.Result.Content = "Filtered, found " + products.Count() + " products";
            products.OrderBy(x => x.Category);


            List<Product> tmpProducts = new List<Product>();
            int iterator = products.Count() / 8 + 1;

            for (int i = 0; i < iterator; i++)
            {
                int j = 1;
                while (tmpProducts.Count() < 8 && products.Count() > j + i * 8 - 1)
                {
                    tmpProducts.Add(products[j + i * 8 - 1]);
                    j++;
                }

                if (tmpProducts.Count() > 0)
                    subMenus.Add(new SubMenu(tmpProducts));

                tmpProducts.Clear();
            }

            MainWindow.Result.Content += ", showing on " + subMenus.Count() + " page(s)";

            if (subMenus.Count() != 0)
            {
                frm_products.Content = subMenus[0];
                subMenuIndex = 0;
                lbl_ActualSubMenuIndex.Content = (subMenuIndex + 1).ToString();
                lbl_SubMenuCount.Content = subMenus.Count().ToString();

                lbl_SubMenuCount.Visibility = Visibility.Visible;
                lbl_ActualSubMenuIndex.Visibility = Visibility.Visible;
                lbl_slash.Visibility = Visibility.Visible;

                if (subMenus.Count() > subMenuIndex + 1)
                {
                    btn_Next.Visibility = Visibility.Visible;
                }
            }
        }

        public void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            if (subMenuIndex < subMenus.Count() - 1)
            {
                frm_products.Content = subMenus[++subMenuIndex];
                btn_Back.Visibility = Visibility.Visible;
                if (subMenuIndex == subMenus.Count() - 1)
                {
                    btn_Next.Visibility = Visibility.Hidden;
                }
                lbl_ActualSubMenuIndex.Content = (subMenuIndex + 1).ToString();
            }

        }

        public void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            if (subMenuIndex > 0)
            {
                
                frm_products.Content = subMenus[--subMenuIndex];
                lbl_ActualSubMenuIndex.Content = (subMenuIndex + 1).ToString();
                btn_Next.Visibility = Visibility.Visible;
                if (subMenuIndex == 0)
                {
                    btn_Back.Visibility = Visibility.Hidden;
                }
                
            }
        }



        public ProductSet GetPreferences()
        {
            ProductSet productSet = new ProductSet();

            if (cb_side.IsChecked.Value)
                productSet.Categories.Add("Side");
            if (cb_gyros.IsChecked.Value)
                productSet.Categories.Add("Gyros");
            if (cb_pizza.IsChecked.Value)
                productSet.Categories.Add("Pizza");
            if (cb_drink.IsChecked.Value)
                productSet.Categories.Add("Drink");
            if (cb_dessert.IsChecked.Value)
                productSet.Categories.Add("Dessert");

            return productSet;
        }
    }
}
