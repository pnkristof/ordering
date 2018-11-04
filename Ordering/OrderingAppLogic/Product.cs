using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingAppLogic
{
    public class Product
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public string ImgUrl { get; set; }

        public string price { get { return "$" + (Price / 100).ToString() + "." + (Price % 100).ToString(); } }

        public override string ToString()
        {
            return Name;
        }
    }
}
