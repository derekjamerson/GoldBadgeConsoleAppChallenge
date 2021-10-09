using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Repo
{
    public class ItemRepo
    {
        List<Item> _listOfItems = new List<Item>();

        public void AddItemToList(Item item)
        {
                _listOfItems.Add(item);
                AssignMenuNumbers();
        }
        public Item GetItem(int num)
        {
            foreach(Item item in _listOfItems)
            {
                if(item.Number == num) { return item; }
            }
            return null;
        }
        public List<Item> GetListOfItems()
        {
            return _listOfItems;
        }
        public bool DeleteItem(int num)
        {
            foreach (Item item in _listOfItems)
            {
                if (item.Number == num) 
                {
                    _listOfItems.Remove(item);
                    AssignMenuNumbers();
                    return true; 
                }
            }
            return false;
        }
        public void AssignMenuNumbers()
        {
            for (int i = 0; i < _listOfItems.Count; i++)
            {
                _listOfItems[i].Number = i + 1;
            }
        }
    }
}
