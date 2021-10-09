using KomodoGreeting.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoGreeting.Console
{
    public class ProgramUI
    {
        CustomerRepo repo = new CustomerRepo();
        public void Run()
        {
            while (true)
            {
                PrintTitle();
                int toTopMainMenu = System.Console.CursorTop;
                System.Console.Write("\n");
                System.Console.Write(new string('_', System.Console.WindowWidth) + "\n");
                int toTopCustomers = System.Console.CursorTop;
                PrintCustomers(-1);
                System.Console.SetCursorPosition(0, toTopMainMenu);
                List<string> _mainMenu = new List<string>() { "Add New", "Update Existing", "Remove Existing", "Exit Application" };
                System.Console.Write(" Main Menu:  ");
                int mainMenu = AskMenu(_mainMenu);
                switch (mainMenu)
                {
                    case 0:
                        DoAddOption();
                        break;
                    case 1:
                        DoUpdateOption(toTopMainMenu, toTopCustomers);
                        break;
                    case 2:
                        DoRemoveOption(toTopMainMenu, toTopCustomers);
                        break;
                    case 3:
                        System.Environment.Exit(0);
                        break;
                }
            }
        }
        public void PrintTitle()
        {
            System.Console.Clear();
            string title = "Komodo Greeting Application";
            System.Console.WriteLine(string.Format("{0," + ((System.Console.WindowWidth / 2) + (title.Length / 2)) + "}", title));
            System.Console.WriteLine("\n");
        }
        public void PrintCustomers(int selection)
        {
            List<Customer> _list = repo.GetList();
            string h1 = "First Name";
            string h2 = "Last Name";
            string h3 = "Type";
            string h4 = "Email";
            System.Console.Write(String.Format("|{0,-12}|{1,-12}|{2,-12}|", h1, h2, h3) + h4 + "\n\n\n");
            for (int i = 0; i < _list.Count; i++)
            {
                Customer c = repo.GetList()[i];
                if (selection == i)
                {
                    System.Console.BackgroundColor = ConsoleColor.White;
                    System.Console.ForegroundColor = ConsoleColor.Black;
                }
                System.Console.Write(String.Format("|{0,-12}|{1,-12}|{2,-12}|", c.FirstName, c.LastName, c.Type) + PrintEmail(c.Type) + "\n\n");
                System.Console.ResetColor();
            }
        }
        public string PrintEmail(Customer.CustomerType type)
        {
            switch (type)
            {
                case Customer.CustomerType.Past:
                    return "It's been a long time since we've heard from you, we want you back.";
                case Customer.CustomerType.Current:
                    return "Thank you for your work with us. We appreciate your loyalty. Here's a coupon.";
                case Customer.CustomerType.Potential:
                    return "We currently have the lowest rates on Helicopter Insurance!";
                default:
                    return new string(' ', System.Console.WindowWidth);
            }
        }
        public int AskMenu(List<string> _options)
        {
            System.Console.CursorVisible = false;
            int toLeft = System.Console.CursorLeft;
            int toTop = System.Console.CursorTop;
            int selection = 0;
            while (true)
            {
                System.Console.SetCursorPosition(toLeft, toTop);
                for (int i = 0; i < _options.Count; i++)
                {
                    if(selection == i)
                    {
                        System.Console.BackgroundColor = ConsoleColor.White;
                        System.Console.ForegroundColor = ConsoleColor.Black;
                    }
                    System.Console.Write(_options[i]);
                    System.Console.ResetColor();
                    if(i != _options.Count - 1)
                    {
                        System.Console.Write("  -  ");
                    }
                }
                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        if(selection > 0)
                        {
                            selection--;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if(selection < _options.Count - 1)
                        {
                            selection++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        return selection;
                }
            }
        }
        public void DoAddOption()
        {
            int toTop = System.Console.CursorTop;
            Customer c = AskCustomer();
            System.Console.Write(String.Format("|{0,-12}|{1,-12}|{2,-12}|", c.FirstName, c.LastName, c.Type) + "  Add this Customer?: ");
            List<string> _yesNo = new List<string>() { "Yes", "No" };
            int confirm = AskMenu(_yesNo);
            if(confirm == 0)
            {
                repo.AddToList(c);
                ClearLine(toTop);
                System.Console.Write(" Customer successfully added.");
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                ClearLine(toTop);
                System.Console.Write(" Customer was NOT added.");
                System.Threading.Thread.Sleep(3000);
            }

        }
        public void ClearLine(int toTop)
        {
            System.Console.SetCursorPosition(0, toTop);
            System.Console.Write(new string(' ', System.Console.WindowWidth));
            System.Console.SetCursorPosition(0, toTop);
        }
        public void DoUpdateOption(int toTopMainMenu, int toTopCustomers)
        {
            System.Console.SetCursorPosition(0, toTopCustomers);
            Customer oldC = PickCustomer();
            System.Console.SetCursorPosition(0, toTopMainMenu);
            System.Console.Write(new string(' ', System.Console.WindowWidth));
            System.Console.SetCursorPosition(0, toTopMainMenu);
            Customer newC = AskCustomer();
            ClearLine(toTopMainMenu);
            System.Console.Write(String.Format("|{0,-12}|{1,-12}|{2,-12}|", newC.FirstName, newC.LastName, newC.Type) + "  Update Customer using these details?: ");
            List<string> _yesNo = new List<string>() { "Yes", "No" };
            int yesNo = AskMenu(_yesNo);
            if (yesNo == 0)
            {
                bool updated = repo.UpdateCustomer(oldC.ID, newC);
                if (updated)
                {
                    ClearLine(toTopMainMenu);
                    System.Console.Write(" Customer successfully updated.");
                    System.Threading.Thread.Sleep(3000);
                }
                else
                {
                    ClearLine(toTopMainMenu);
                    System.Console.Write(" Customer was NOT updated.");
                    System.Threading.Thread.Sleep(3000);
                }
            }
            else
            {
                ClearLine(toTopMainMenu);
                System.Console.Write(" Customer was NOT updated.");
                System.Threading.Thread.Sleep(3000);
            }
        }
        public Customer AskCustomer()
        {
            System.Console.CursorVisible = true;
            int toTop = System.Console.CursorTop;
            ClearLine(toTop);
            System.Console.Write(" First Name: ");
            string fName = System.Console.ReadLine();
            ClearLine(toTop);
            System.Console.Write(" Last Name: ");
            string lName = System.Console.ReadLine();
            ClearLine(toTop);
            System.Console.Write(" Type: ");
            List<string> _types = new List<string>();
            foreach (string name in Enum.GetNames(typeof(Customer.CustomerType)))
            {
                _types.Add(name);
            }
            Customer.CustomerType cType = (Customer.CustomerType)AskMenu(_types);
            Customer c = new Customer(fName, lName, cType);
            ClearLine(toTop);
            return c;
        }
        public Customer PickCustomer()
        {
            System.Console.CursorVisible = false;
            int toTop = System.Console.CursorTop;
            int selection = 0;
            while (true)
            {
                System.Console.SetCursorPosition(0, toTop);
                PrintCustomers(selection);
                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        if(selection < repo.GetList().Count - 1)
                        {
                            selection++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if(selection > 0)
                        {
                            selection--;
                        }
                        break;
                    case ConsoleKey.Enter:
                        return repo.GetList()[selection];
                }
            }
        }
        public void DoRemoveOption(int toTopMainMenu, int toTopCustomers)
        {
            System.Console.SetCursorPosition(0, toTopCustomers);
            Customer customer = PickCustomer();
            ClearLine(toTopMainMenu);
            System.Console.Write(" Remove Customer?: ");
            List<string> _yesNo = new List<string>() { "Yes", "No" };
            int yesNo = AskMenu(_yesNo);
            if (yesNo == 0)
            {
                bool removed = repo.RemoveCustomer(customer.ID);
                if (removed)
                {
                    ClearLine(toTopMainMenu);
                    System.Console.Write(" Customer successfully removed.");
                    System.Threading.Thread.Sleep(3000);
                }
                else
                {
                    ClearLine(toTopMainMenu);
                    System.Console.Write(" Customer was NOT removed.");
                    System.Threading.Thread.Sleep(3000);
                }
            }
            else
            {
                ClearLine(toTopMainMenu);
                System.Console.Write(" Customer was NOT removed.");
                System.Threading.Thread.Sleep(3000);
            }
        }
    }
}
