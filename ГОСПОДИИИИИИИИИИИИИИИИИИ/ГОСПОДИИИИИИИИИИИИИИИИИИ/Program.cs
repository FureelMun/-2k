using System;
using System.Collections.Generic;
using System.Data;

// Роль пользователя
enum UserRole
{
    Administrator,
    Cashier,
    PersonnelManager,
    WarehouseManager,
    Accountant
}

// Класс для пользователя
class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string EmployeeName { get; set; }
}

class Program
{
    static List<User> users = new List<User>
    {
        new User { Username = "admin", Password = "admin123", Role = UserRole.Administrator },
        new User { Username = "cashier", Password = "cashier123", Role = UserRole.Cashier },
        new User { Username = "personnel", Password = "personnel123", Role = UserRole.PersonnelManager },
        new User { Username = "warehouse", Password = "warehouse123", Role = UserRole.WarehouseManager },
        new User { Username = "accountant", Password = "accountant123", Role = UserRole.Accountant }
    };

    static void Main(string[] args)
    {
        bool isAuthenticated = false;
        User currentUser = null;

        while (!isAuthenticated)
        {
            Console.WriteLine("Введите логин:");
            string username = Console.ReadLine();
            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();

            currentUser = AuthenticateUser(username, password);

            if (currentUser != null)
            {
                isAuthenticated = true;
            }
            else
            {
                Console.WriteLine("Неверный логин или пароль. Попробуйте снова.");
                Console.WriteLine("Хотите зарегестрироваться");
                int a = Convert.ToInt32(Console.ReadLine());
                if (a == 1) { CreateUser(); }

            }
        }

        Console.WriteLine("Вы успешно авторизовались!");

        // Выводим информацию в зависимости от роли пользователя
        switch (currentUser.Role)
        {
            case UserRole.Administrator:
                ShowAdministratorMenu();
                break;
            case UserRole.Cashier:
                ShowCashierMenu();
                break;
            case UserRole.PersonnelManager:
                ShowPersonnelManagerMenu();
                break;
            case UserRole.WarehouseManager:
                ShowWarehouseManagerMenu();
                break;
            case UserRole.Accountant:
                ShowAccountantMenu();
                break;
            default:
                Console.WriteLine("У пользователя нет назначенной роли.");
                break;
        }

        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static User AuthenticateUser(string username, string password)
    {
        return users.FirstOrDefault(u => u.Username == username && u.Password == password);
    }

    static void ShowAdministratorMenu()
    {
        Console.WriteLine("Меню администратора:");
        Console.WriteLine("1. Создать нового пользователя");
        Console.WriteLine("2. Удалить существующего пользователя");
        Console.WriteLine("3. Изменить роль пользователя");
        Console.WriteLine("4. Изменить пароль пользователя");
        Console.WriteLine("--");
        Console.WriteLine("0. Выйти");

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Введите номер пункта меню:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateUser();
                    break;
                case "2":
                    DeleteUser();
                    break;
                case "3":
                    ChangeUserRole();
                    break;
                case "4":
                    ChangeUserPassword();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void ShowCashierMenu()
    {
        Console.WriteLine("Меню кассира:");
        Console.WriteLine("1. Создать новый чек");
        Console.WriteLine("2. Оформить возврат");
        Console.WriteLine("3. Печать отчета о продажах");
        Console.WriteLine("--");
        Console.WriteLine("0. Выйти");

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Введите номер пункта меню:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    break;
                case "2":

                    break;
                case "3":

                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void ShowPersonnelManagerMenu()
    {
        Console.WriteLine("Меню менеджера по персоналу:");
        Console.WriteLine("1. Добавить нового сотрудника");
        Console.WriteLine("2. Удалить сотрудника");
        Console.WriteLine("3. Редактировать информацию о сотруднике");
        Console.WriteLine("--");
        Console.WriteLine("0. Выйти");

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Введите номер пункта меню:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateUserProd();

                    break;
                case "2":
                    DeleteUserProd();
                    break;
                case "3":
                    Console.WriteLine("Введите логин пользователя:");
                    string username = Console.ReadLine();
                    Console.WriteLine("Введите нужную информацию:");
                    string a = Console.ReadLine();
                    Console.WriteLine("Информация успешно занесена");
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
    static void DeleteUserProd()
    {
        Console.WriteLine("Введите логин пользователя, которого нужно удалить:");
        string username = Console.ReadLine();

        User userToDelete = users.FirstOrDefault(u => u.Username == username);

        if (userToDelete != null)
        {
            users.Remove(userToDelete);
            Console.WriteLine("Пользователь успешно удален!");
        }
        else
        {
            Console.WriteLine("Пользователь не найден.");
        }
    }
    static void CreateUserProd()
    {
        Console.WriteLine("Введите логин нового пользователя:");
        string username = Console.ReadLine();

        Console.WriteLine("Введите пароль нового пользователя:");
        string password = Console.ReadLine();


        UserRole role = UserRole.Cashier;

    }
        static void ShowWarehouseManagerMenu()
    {
        Console.WriteLine("Меню менеджера склада:");
        Console.WriteLine("1. Добавить новый товар");
        Console.WriteLine("2. Удалить товар");
        Console.WriteLine("3. Редактировать информацию о товаре");
        Console.WriteLine("--");
        Console.WriteLine("4. Просмотреть текущий уровень запасов");
        Console.WriteLine("5. Заказать новую партию товаров");
        Console.WriteLine("--");
        Console.WriteLine("0. Выйти");

        // Создаем матрицу для хранения информации о товарах
        string[,] warehouse = new string[100, 3]; // предполагаем, что у нас может быть до 100 товаров, каждый товар имеет 3 характеристики

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Введите номер пункта меню:");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddNewProduct(warehouse);
                    break;
                case "2":
                    RemoveProduct(warehouse);
                    break;
                case "3":
                    EditProduct(warehouse);
                    break;
                case "4":
                    ShowStockLevel(warehouse);
                    break;
                case "5":
                    PlaceOrder();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void AddNewProduct(string[,] warehouse)
    {
        // Запросить у пользователя информацию о новом товаре
        Console.WriteLine("Введите информацию о новом товаре:");
        Console.Write("Название товара: ");
        string name = Console.ReadLine();
        Console.Write("Количество товара: ");
        string quantity = Console.ReadLine();
        Console.Write("Цена товара: ");
        string price = Console.ReadLine();

        // Найти первую свободную ячейку в матрице и добавить информацию о товаре
        for (int i = 0; i < warehouse.GetLength(0); i++)
        {
            if (warehouse[i, 0] == null)
            {
                warehouse[i, 0] = name;
                warehouse[i, 1] = quantity;
                warehouse[i, 2] = price;
                Console.WriteLine("Товар успешно добавлен.");
                return;
            }
        }

        Console.WriteLine("Невозможно добавить товар. Матрица заполнена.");
    }

    static void RemoveProduct(string[,] warehouse)
    {
        // Запросить у пользователя название товара, который нужно удалить
        Console.Write("Введите название товара, который нужно удалить: ");
        string name = Console.ReadLine();

        // Найти товар в матрице и удалить его
        for (int i = 0; i < warehouse.GetLength(0); i++)
        {
            if (warehouse[i, 0] == name)
            {
                warehouse[i, 0] = null;
                warehouse[i, 1] = null;
                warehouse[i, 2] = null;
                Console.WriteLine("Товар успешно удален.");
                return;
            }
        }

        Console.WriteLine("Товар не найден.");
    }

    static void EditProduct(string[,] warehouse)
    {
        // Запросить у пользователя название товара, который нужно отредактировать
        Console.Write("Введите название товара, который нужно отредактировать: ");
        string name = Console.ReadLine();

        // Найти товар в матрице и позволить пользователю отредактировать его информацию
        for (int i = 0; i < warehouse.GetLength(0); i++)
        {
            if (warehouse[i, 0] == name)
            {
                Console.WriteLine("Введите новую информацию о товаре:");
                Console.Write("Название товара: ");
                warehouse[i, 0] = Console.ReadLine();
                Console.Write("Количество товара: ");
                warehouse[i, 1] = Console.ReadLine();
                Console.Write("Цена товара: ");
                warehouse[i, 2] = Console.ReadLine();
                Console.WriteLine("Товар успешно отредактирован.");
                return;
            }
        }

        Console.WriteLine("Товар не найден.");
    }

    static void ShowStockLevel(string[,] warehouse)
    {
        Console.WriteLine("Текущий уровень запасов:");

        // Вывести информацию о товарах на складе
        for (int i = 0; i < warehouse.GetLength(0); i++)
        {
            if (warehouse[i, 0] != null)
            {
                Console.WriteLine($"Название товара: {warehouse[i, 0]}, Количество: {warehouse[i, 1]}, Цена: {warehouse[i, 2]}");
            }
        }
    }

    static void PlaceOrder()
    {
        
        Console.WriteLine("Функция размещения заказа на новую партию товаров.");
    }


    static void ShowAccountantMenu()
{
    Console.WriteLine("Меню бухгалтера:");
    Console.WriteLine("1. Просмотреть состояние финансов");
    Console.WriteLine("2. Сформировать отчеты");
    Console.WriteLine("--");
    Console.WriteLine("0. Выйти");

    bool exit = false;

    while (!exit)
    {
        Console.WriteLine("Введите номер пункта меню:");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.WriteLine("Все плохо:");
                break;
            case "2":
                Console.WriteLine("Отчет успешно создан");
                break;
            case "0":
                exit = true;
                break;
            default:
                Console.WriteLine("Неверный выбор. Попробуйте снова.");
                break;
        }
    }
}

    static void CreateUser()
    {
        Console.WriteLine("Введите логин нового пользователя:");
        string username = Console.ReadLine();

        Console.WriteLine("Введите пароль нового пользователя:");
        string password = Console.ReadLine();

        Console.WriteLine("Выберите роль нового пользователя:");
        Console.WriteLine("1. Администратор");
        Console.WriteLine("2. Кассир");
        Console.WriteLine("3. Менеджер по персоналу");
        Console.WriteLine("4. Менеджер склада");
        Console.WriteLine("5. Бухгалтер");

        UserRole role = UserRole.Cashier;
        bool validRole = false;

        while (!validRole)
        {
            string roleChoice = Console.ReadLine();

            switch (roleChoice)
            {
                case "1":
                    role = UserRole.Administrator;
                    validRole = true;
                    break;
                case "2":
                    role = UserRole.Cashier;
                    validRole = true;
                    break;
                case "3":
                    role = UserRole.PersonnelManager;
                    validRole = true;
                    break;
                case "4":
                    role = UserRole.WarehouseManager;
                    validRole = true;
                    break;
                case "5":
                    role = UserRole.Accountant;
                    validRole = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }

        User newUser = new User
        {
            Username = username,
            Password = password,
            Role = role
        };

        users.Add(newUser);

        Console.WriteLine("Новый пользователь успешно создан!");
    }

    static void DeleteUser()
    {
        Console.WriteLine("Введите логин пользователя, которого нужно удалить:");
        string username = Console.ReadLine();

        User userToDelete = users.FirstOrDefault(u => u.Username == username);

        if (userToDelete != null)
        {
            users.Remove(userToDelete);
            Console.WriteLine("Пользователь успешно удален!");
        }
        else
        {
            Console.WriteLine("Пользователь не найден.");
        }
    }

    static void ChangeUserRole()
    {
        Console.WriteLine("Введите логин пользователя, роль которого нужно изменить:");
        string username = Console.ReadLine();

        User userToChangeRole = users.FirstOrDefault(u => u.Username == username);

        if (userToChangeRole != null)
        {
            Console.WriteLine("Выберите новую роль пользователя:");
            Console.WriteLine("1. Администратор");
            Console.WriteLine("2. Кассир");
            Console.WriteLine("3. Менеджер по персоналу");
            Console.WriteLine("4. Менеджер склада");
            Console.WriteLine("5. Бухгалтер");

            UserRole newRole = UserRole.Cashier;
            bool validRole = false;

            while (!validRole)
            {
                string roleChoice = Console.ReadLine();

                switch (roleChoice)
                {
                    case "1":
                        newRole = UserRole.Administrator;
                        validRole = true;
                        break;
                    case "2":
                        newRole = UserRole.Cashier;
                        validRole = true;
                        break;
                    case "3":
                        newRole = UserRole.PersonnelManager;
                        validRole = true;
                        break;
                    case "4":
                        newRole = UserRole.WarehouseManager;
                        validRole = true;
                        break;
                    case "5":
                        newRole = UserRole.Accountant;
                        validRole = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }

            userToChangeRole.Role = newRole;

            Console.WriteLine("Роль пользователя успешно изменена!");
        }
        else
        {
            Console.WriteLine("Пользователь не найден.");
        }
    }

    static void ChangeUserPassword()
    {
        Console.WriteLine("Введите логин пользователя, пароль которого нужно изменить:");
        string username = Console.ReadLine();

        User userToChangePassword = users.FirstOrDefault(u => u.Username == username);

        if (userToChangePassword != null)
        {
            Console.WriteLine("Введите новый пароль пользователя:");
            string newPassword = Console.ReadLine();

            userToChangePassword.Password = newPassword;

            Console.WriteLine("Пароль пользователя успешно изменен!");
        }
        else
        {
            Console.WriteLine("Пользователь не найден.");
        }
    }
}
    
     