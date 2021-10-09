using KomodoGreeting.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KomodoGreetingTests
{
    [TestClass]
    public class CustomerRepoTests
    {
        CustomerRepo repo = new CustomerRepo();
        [TestInitialize]
        public void Arrange()
        {
            Customer customer1 = new Customer("Test", "Subject", Customer.CustomerType.Current);
            Customer customer2 = new Customer("Subject", "Num2", Customer.CustomerType.Potential);
            repo.AddToList(customer1);
            repo.AddToList(customer2);
        }
        [TestMethod]
        public void AddToList_CustomerDoesExist_True()
        {
            Customer customer = new Customer("Unit", "Test", Customer.CustomerType.Current);
            repo.AddToList(customer);

            CollectionAssert.Contains(repo.GetList(), customer);
        }
        [TestMethod]
        public void AddToList_IdsAreUnique_True()
        {
            Assert.AreNotEqual(repo.GetList()[0].ID, repo.GetList()[1].ID);
        }
        [TestMethod]
        public void GetCustomer_CustomerDoesNotExist_True()
        {
            Assert.IsNull(repo.GetCustomer(-1));
        }
        [TestMethod]
        public void UpdateCustomer_CustomerDoesNotExist_True()
        {
            Customer newC = new Customer("first", "last", Customer.CustomerType.Past);
            Assert.IsFalse(repo.UpdateCustomer(-1, newC));
        }
        [TestMethod]
        public void UpdateCustomer_CustomerDoesExist_True()
        {
            Customer customer = repo.GetList()[0];
            Customer newC = new Customer("f", "l", Customer.CustomerType.Past);
            repo.UpdateCustomer(customer.ID, newC);
            Assert.IsTrue(repo.GetCustomer(customer.ID).FullName == "f l");
        }
    }
}
