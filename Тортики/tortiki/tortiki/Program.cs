using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        CakeOrder order = new CakeOrder();
        order.StartOrder();
        ConsoleKeyInfo key = Console.ReadKey();
        if (key.Key == ConsoleKey.S)
        {
            order.SaveOrder();
        }
    }
}

class CakeOrder
{
    private List<string> menuItems;
    private List<List<MenuItem>> subMenuItems;
    private int selectedItem;
    private int selectedSubMenuIndex;
    private decimal totalPrice;
    private List<string> orderList;

    public CakeOrder()
    {
        menuItems = new List<string> { "Форма", "Размер", "Вкус", "Количество", "Глазурь", "Декор" };
        subMenuItems = new List<List<MenuItem>>
        {
            new List<MenuItem>
            {
                new MenuItem("Круглая", 100),
                new MenuItem("Квадратная", 150),
                new MenuItem("Прямоугольная", 200)
            },
            new List<MenuItem>
            {
                new MenuItem("Маленький", 50),
                new MenuItem("Средний", 100),
                new MenuItem("Большой", 150)
            },
            new List<MenuItem>
            {
                new MenuItem("Шоколадный", 75),
                new MenuItem("Ванильный", 50),
                new MenuItem("Фруктовый", 100)
            },
            new List<MenuItem>
            {
                new MenuItem("1", 10),
                new MenuItem("2", 20),
                new MenuItem("3", 30)
            },
            new List<MenuItem>
            {
                new MenuItem("Шоколадная", 25),
                new MenuItem("Карамельная", 30),
                new MenuItem("Фруктовая", 20)
            },
            new List<MenuItem>
            {
                new MenuItem("Цветы", 50),
                new MenuItem("Фигурки", 75),
                new MenuItem("Надпись", 100)
            }
        };

        selectedItem = 0;
        selectedSubMenuIndex = 0;
        totalPrice = 0;
        orderList = new List<string>();
    }

    public void StartOrder()
    {
        bool online = true;

        while (online)
        {
            Console.Clear();
            Console.WriteLine("Выберите пункт:");

            for (int i = 0; 
                i < menuItems.Count; i++)
            {
                if (i == selectedItem)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("-> " + menuItems[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("   " + menuItems[i]);
                }
            
            }

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.UpArrow && selectedItem > 0)
            {
                selectedItem--;
            }
            else if (key.Key == ConsoleKey.DownArrow && selectedItem < menuItems.Count - 1)
            {
                selectedItem++;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                EnterSubMenu();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                online = false;
            }
            if (key.Key == ConsoleKey.S)
            {
                SaveOrder();
            }
        }
    }

    private void EnterSubMenu()
    {
        bool inSubMenu = true;

        while (inSubMenu)
        {
            Console.Clear();
            Console.WriteLine("Выберите подпункт:");

            List<MenuItem> subMenu = subMenuItems[selectedItem];

            for (int i = 0; i < subMenu.Count; i++)
            {
                if (i == selectedSubMenuIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("-> " + subMenu[i].Description + " - " + subMenu[i].Price + " руб.");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("   " + subMenu[i].Description + " - " + subMenu[i].Price + " руб.");
                }
            }

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.UpArrow && selectedSubMenuIndex > 0)
            {
                selectedSubMenuIndex--;
            }
            else if (key.Key == ConsoleKey.DownArrow && selectedSubMenuIndex < subMenu.Count - 1)
            {
                selectedSubMenuIndex++;
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                MenuItem selectedMenuItem = subMenu[selectedSubMenuIndex];
                totalPrice += selectedMenuItem.Price;
                orderList.Add(selectedMenuItem.Description);
                Console.WriteLine("Выбрано: " + selectedMenuItem.Description);
                Console.WriteLine("Сумма заказа: " + totalPrice + " руб.");
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.WriteLine("Чтобы сохранить нажмите Spacebar.");
                if (key.Key == ConsoleKey.Spacebar)
                {
                    SaveOrder();
                    Console.WriteLine(".");
                }
                Console.ReadLine();
                inSubMenu = false;
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                inSubMenu = false;
            }
        }
    }

    public void SaveOrder()
    {
        string fileName = "order_history.txt";
        string orderDetails = string.Join(", ", orderList);
        string orderLine = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " - " + orderDetails + " - " + totalPrice + " руб.";

        using (StreamWriter sw = File.AppendText(fileName))
        {
            sw.WriteLine(orderLine);
        }

        Console.WriteLine("Заказ сохранен в файле " + fileName);
    }
}

class MenuItem
{
    public string Description { get; set; }
    public decimal Price { get; set; }

    public MenuItem(string description, decimal price)
    {
        Description = description;
        Price = price;

    }
}