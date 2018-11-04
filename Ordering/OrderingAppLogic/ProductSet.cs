using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingAppLogic
{
    public class ProductSet
    {
        public List<string> Categories { get; set; }
        public ProductSet()
        {
            Categories = new List<string>();
        }
    }
}
