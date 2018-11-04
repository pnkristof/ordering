using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingAppLogic
{
    public class ProductAndQuantity
    {
        public Product product { get; set; }
        public int quantity { get; set; }
    }

    public class Cart
    {
        public static List<ProductAndQuantity> Set = new List<ProductAndQuantity>();

        public static void Add(Product product)
        {
            if (Set.Any(x => x.product.Id == product.Id))
            {
                (from x in Set
                 where x.product.Id == product.Id
                 select x).First().quantity++;
            }
            else
            {
                Set.Add(new ProductAndQuantity { product = product, quantity = 1 });
            }
        }

        public static int total
        {
            get
            {
                return Set.Sum(x => x.quantity * x.product.Price);
            }
        }

        public static string showTotal { get { return "$" + (total / 100).ToString() + "." + (total % 100).ToString(); } }
    }
}
