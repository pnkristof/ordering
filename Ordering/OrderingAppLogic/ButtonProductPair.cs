using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OrderingAppLogic
{
    public class ButtonProductPair
    {
        public int Id { get; set; }
        public Button button { get; set; }
        public Product product { get; set; }

        public ButtonProductPair(Button button, Product product)
        {
            this.button = button;
            this.product = product;
        }
    }
}
