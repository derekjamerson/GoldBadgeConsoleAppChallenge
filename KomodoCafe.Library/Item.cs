using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Repo
{
    public class Item
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public double Price { get; set; }
        public Item(string name, string descr, List<string> _ingred, double price)
        {
            Name = name;
            Description = descr;
            Ingredients = _ingred;
            Price = price;
        }
    }
}
