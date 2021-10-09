using Menu.Repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Menu.Tests
{
    [TestClass]
    public class ItemRepoTests
    {
        ItemRepo repo = new ItemRepo();
        List<string> _ingredients = new List<string>();
        [TestMethod]
        public void AddToList_ItemExists_True()
        {
            Item item = new Item("name", "description", _ingredients, 9.99d);
            repo.AddItemToList(item);
            Assert.AreSame(repo.GetItem(1), item);
        }
        [TestMethod]
        public void GetItem_ItemExists_True()
        {
            Item item = new Item("name", "description", _ingredients, 9.99d);
            repo.AddItemToList(item);
            Item testItem = repo.GetItem(1);
            Assert.AreSame(item, testItem);
        }
        [TestMethod]
        public void GetItem_ItemDoesNotExist_True()
        {
            Item item = new Item("name", "description", _ingredients, 9.99d);
            repo.AddItemToList(item);
            Assert.IsNull(repo.GetItem(2));
        }
        [TestMethod]
        public void GetListOfItems_ListExists_True()
        {
            List < Item > _list = repo.GetListOfItems();
            Assert.IsNotNull(_list);
        }
        [TestMethod]
        public void DeleteItem_ItemExists_True()
        {
            Item item = new Item("name", "description", _ingredients, 9.99d);
            repo.AddItemToList(item);
            Assert.IsTrue(repo.DeleteItem(1));
        }
        [TestMethod]
        public void DeleteItem_ItemDoesNotExist_True()
        {
            Assert.IsFalse(repo.DeleteItem(1));
        }
        [TestMethod]
        public void AssignMenuNumbers_NumbersAreSequential_True()
        {
            Item item = new Item("name", "description", _ingredients, 9.99d);
            repo.AddItemToList(item);
            Item item1 = new Item("name1", "description1", _ingredients, 99.99d);
            repo.AddItemToList(item1);

            Assert.IsTrue(item1.Number - item.Number == 1);
        }
    }
}
