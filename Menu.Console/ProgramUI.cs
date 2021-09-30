using Menu.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Console
{
    public class ProgramUI
    {
        ItemRepo repo = new ItemRepo();
        public void Run()
        {
            while (true)
            {
                int menuSelection = MainMenu();
                switch (menuSelection)
                {
                    case 1:
                        DoViewMenu();
                        break;
                    case 2:
                        DoAddMenu();
                        break;
                    case 3:
                        DoDeleteMenu();
                        break;
                    case 4:
                        System.Environment.Exit(0);
                        break;
                }
            }
        }
        public int MainMenu()
        {
            System.Console.CursorVisible = false;
            PrintTitle();
            System.Console.WriteLine(" 1. View All Menu Items\n\n 2. Add New Menu Item\n\n 3. Delete Menu Item\n\n 4. Exit");
            while (true)
            {
                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        return 1;
                    case ConsoleKey.D2:
                        return 2;
                    case ConsoleKey.D3:
                        return 3;
                    case ConsoleKey.D4:
                        return 4;
                    default:
                        break;
                }
            }
        }
        public void PrintTitle()
        {
            System.Console.Clear();
            string title = "Komodo Cafe Menu Management";
            System.Console.WriteLine(String.Format("{0," + ((System.Console.WindowWidth / 2) + (title.Length / 2)) + "}", title));
            System.Console.WriteLine("\n\n");
        }
        public void DoViewMenu()
        {
            System.Console.CursorVisible = false;
            PrintTitle();
            foreach(Item item in repo.GetListOfItems())
            {
                PrintItem(item);
            }
            System.Console.ReadKey(true);
        }
        public void DoAddMenu()
        {
            System.Console.CursorVisible = true;
            PrintTitle();
            System.Console.Write(" Add New Menu Item\n");
            System.Console.Write("\n Name: ");
            string name = System.Console.ReadLine();
            System.Console.Write("\n Description: ");
            string descr = System.Console.ReadLine();
            double price = AskPrice();
            List<string> _ingred = AskIngredients();
            Item newItem = new Item(name, descr, _ingred, price);
            PrintTitle();
            PrintItem(newItem);
            System.Console.Write("\n\n\n\n");
            string sure = "Add this Menu Item?";
            System.Console.Write(String.Format("{0," + ((System.Console.WindowWidth / 2) + (sure.Length / 2)) + "}", sure + "\n"));
            bool confirm = AskYesNo();
            if (confirm)
            {
                repo.AddItemToList(newItem);
                System.Console.CursorVisible = false;
                string success = "Item successfully added.";
                System.Console.Write("\n\n");
                System.Console.Write(String.Format("{0," + ((System.Console.WindowWidth / 2) + (success.Length / 2)) + "}", success));
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                System.Console.CursorVisible = false;
                string fail = "Item was not added.";
                System.Console.Write("\n\n");
                System.Console.Write(String.Format("{0," + ((System.Console.WindowWidth / 2) + (fail.Length / 2)) + "}", fail));
                System.Threading.Thread.Sleep(3000);
            }
        }
        public void DoDeleteMenu()
        {
            System.Console.CursorVisible = true;
            PrintTitle();
            System.Console.WriteLine(" Delete A Menu Item\n\n");
            System.Console.Write(" Menu Number: ");
            int choice = (int)ReadForDouble();
            Item item = repo.GetItem(choice);
            if(item != null)
            {
                System.Console.WriteLine("\n");
                PrintItem(item);
                string verify = "Delete this item?";
                System.Console.WriteLine("\n\n");
                System.Console.WriteLine(String.Format("{0," + ((System.Console.WindowWidth / 2) + (verify.Length / 2)) + "}", verify));
                int toTop = System.Console.CursorTop;
                if(AskYesNo())
                {
                    repo.DeleteItem(choice);
                    string success = "Item successfully deleted.";
                    System.Console.WriteLine("\n");
                    System.Console.WriteLine(String.Format("{0," + ((System.Console.WindowWidth / 2) + (success.Length / 2)) + "}", success));
                    System.Threading.Thread.Sleep(3000);
                }
                else
                {
                    string fail = "Item was NOT deleted.";
                    System.Console.WriteLine("\n");
                    System.Console.WriteLine(String.Format("{0," + ((System.Console.WindowWidth / 2) + (fail.Length / 2)) + "}", fail));
                    System.Threading.Thread.Sleep(3000);
                }

            }
            else
            {
                string notFound = "Item not found.";
                System.Console.WriteLine("\n\n");
                System.Console.WriteLine(String.Format("{0," + ((System.Console.WindowWidth / 2) + (notFound.Length / 2)) + "}", notFound));
                System.Threading.Thread.Sleep(3000);
            }

        }
        public List<string> AskIngredients()
        {
            System.Console.CursorVisible = true;
            List<string> _ingred = new List<string>();
            System.Console.Write("\n Ingredients: ");
            int toLeft = System.Console.CursorLeft;
            int toTop = System.Console.CursorTop;
            int lineDown = 0;
            while(true)
            {
                string response = System.Console.ReadLine();
                if (!string.IsNullOrEmpty(response))
                {
                    System.Console.SetCursorPosition(toLeft, (toTop + ++lineDown));
                    _ingred.Add(response);
                }
                else
                {
                    if (_ingred.Any())
                    {
                        return _ingred;
                    }
                    System.Console.SetCursorPosition(toLeft, toTop);
                }
            }

        }
        public double AskPrice()
        {
            System.Console.Write("\n Price: ");
            return Math.Round(ReadForDouble(), 2);
        }
        public double ReadForDouble()
        {
            int toLeft = System.Console.CursorLeft;
            int toTop = System.Console.CursorTop;
            double output = default;
            while (true)
            {
                
                string response = System.Console.ReadLine();
                if (double.TryParse(response, out output))
                {
                    return output;
                }
                else
                {
                    System.Console.SetCursorPosition(toLeft, toTop);
                    System.Console.WriteLine(new string(' ', System.Console.WindowWidth));
                    System.Console.SetCursorPosition(toLeft, toTop);
                }
            }
        }
        public void PrintItem(Item item)
        {
            System.Console.WriteLine($" #{item.Number}  {item.Name}  ${item.Price}\n Description: {item.Description}\n Ingredients: {string.Join(", ", item.Ingredients)}\n\n");
        }
        public bool AskYesNo()
        {
            System.Console.CursorVisible = false;
            int toTop = System.Console.CursorTop;
            bool selected = false;
            bool yesOrNo = default;
            string yString = "YES";
            string nString = "NO";
            while (true)
            {
                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Y:
                    case ConsoleKey.D1:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.T:
                        selected = true;
                        yesOrNo = true;
                        System.Console.ResetColor();
                        System.Console.SetCursorPosition(0, toTop);
                        System.Console.Write(new string(' ', System.Console.WindowWidth));
                        System.Console.SetCursorPosition(0, toTop);
                        System.Console.Write(String.Format("{0," + ((System.Console.WindowWidth / 2) + (yString.Length / 2)) + "}", ""));
                        System.Console.BackgroundColor = ConsoleColor.White;
                        System.Console.ForegroundColor = ConsoleColor.Black;
                        System.Console.WriteLine(yString);
                        break;
                    case ConsoleKey.N:
                    case ConsoleKey.D2:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.F:
                        selected = true;
                        yesOrNo = false;
                        System.Console.ResetColor();
                        System.Console.SetCursorPosition(0, toTop);
                        System.Console.Write(new string(' ', System.Console.WindowWidth));
                        System.Console.SetCursorPosition(0, toTop);
                        System.Console.Write(String.Format("{0," + ((System.Console.WindowWidth / 2) + (nString.Length / 2)) + "}", ""));
                        System.Console.BackgroundColor = ConsoleColor.White;
                        System.Console.ForegroundColor = ConsoleColor.Black;
                        System.Console.WriteLine(nString);
                        break;
                    default:
                        if (selected) { System.Console.ResetColor(); return yesOrNo; }
                        break;
                }
            }
        }
    }
}
