using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
ConsoleKeyInfo txt;
DateTime date1 = DateTime.Now;
bool work = true;
int delta = 0;
Dictionary<DateTime, string> Dictionar = new Dictionary<DateTime, string>();

try { 
    string fromFile = File.ReadAllText("C:\\Users\\Елизавета\\OneDrive\\Рабочий стол\\МПТ\\ОАИП(шарп)\\Ежедневник\\TimeDay\\TimeDay\\f.json");
    Dictionar = JsonConvert.DeserializeObject<Dictionary<DateTime, string>>(fromFile);
}
finally
{
    Console.WriteLine("kik");
}
void Rep()
{
    Console.Clear();
    Console.WriteLine("Выбранная дата: ");
    Console.WriteLine(date1.AddDays(delta));
    Console.Write("Заметка: ");
    foreach (var item in Dictionar)
    {
        if (item.Key.Date == date1.AddDays(delta).Date)
            Console.WriteLine(item.Value);
    }
}
int Data_is(DateTime date1)
{
    txt = Console.ReadKey(true);

    Value_dict();

    switch (txt.Key)
    {
        case ConsoleKey.Q:
            delta = 0;
            try
            {
                Console.WriteLine(Dictionar[date1.AddDays(delta)]);
            }
            catch
            {
                Console.WriteLine(date1.AddDays(delta));
            }

            break;
        case ConsoleKey.RightArrow:
            delta += 1;

            break;

        case ConsoleKey.LeftArrow:
            delta -= 1;
            break;
    }
    Rep();

    return delta;
}
void Value_dict()
{
    if (txt.Key == ConsoleKey.Enter)
    {   string a = Console.ReadLine();

        if (Dictionar.ContainsKey(date1.AddDays(delta)) ) {
            
           Dictionar[date1.AddDays(delta)] = Convert.ToString(Dictionar[date1.AddDays(delta)]) + a;
        }
        else
        {
            Dictionar.Add(date1.AddDays(delta), a);
        }
    }
    if (Dictionar.ContainsKey(date1.AddDays(delta)))
    {
        switch (txt.Key)
        {
            case ConsoleKey.Delete:
                Dictionar.Remove(date1.AddDays(delta));
                break;
            case ConsoleKey.OemPlus:
                Dictionar[date1.AddDays(delta)] = Convert.ToString(Dictionar[date1.AddDays(delta)]) + Environment.NewLine;
                break;

            case ConsoleKey.OemMinus:
                Dictionar[date1.AddDays(delta)] =Environment.NewLine + Convert.ToString(Dictionar[date1.AddDays(delta)]);
                break;
        }
    }
}
while (work)
{
    delta = Data_is(date1);

    if (txt.Key == ConsoleKey.Escape)
    {
        work = false;
        return;
    }
}
string json = JsonConvert.SerializeObject(Dictionar);
File.WriteAllText("f.json", json);