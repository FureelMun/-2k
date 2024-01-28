using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading;
using System;


class Program
{
    static Stopwatch stopwatch;
    static string typedText;
    static bool isTimerExpired;
    static bool isTextCompleted;

    static void Main(string[] args)
    {
        Console.Write("Введите Ваше имя: ");
        string name = Console.ReadLine();

        Console.WriteLine("весной все на земле пробуждается к новой жизни. растаивает снег. появляется молодая зеленая травка.");
        Console.WriteLine("--------------------------");

        typedText = "";
        isTimerExpired = false;
        isTextCompleted = false;

        Thread inputThread = new Thread(InputThread);
        inputThread.Start();

        stopwatch = Stopwatch.StartNew();
        TimeSpan timerDuration = TimeSpan.FromMinutes(1);

        while (!isTimerExpired && !isTextCompleted)
        {
            TimeSpan remainingTime = timerDuration - stopwatch.Elapsed;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"Осталось времени: {remainingTime:mm\\:ss}");

            if (remainingTime <= TimeSpan.Zero)
            {
                isTimerExpired = true;
                Console.WriteLine("\nВремя вышло!");
                break;
            }

            Thread.Sleep(1000);
        }

        stopwatch.Stop();
        inputThread.Join();

        if (isTextCompleted)
        {
            int totalChars = typedText.Length;
            int charsPerMinute = (int)(totalChars / stopwatch.Elapsed.TotalMinutes);
            int charsPerSecond = (int)(totalChars / stopwatch.Elapsed.TotalSeconds);

            Console.WriteLine("\nТест завершен!");
            Console.WriteLine($"Скорость печати: {charsPerMinute} символов в минуту, {charsPerSecond} символов в секунду");

            // Сохранение результатов в таблицу рекордов
            RecordTable.AddRecord(new Record(name, charsPerMinute, charsPerSecond));
        }
        else
        {
            Console.WriteLine("\nВы не успели, но Вы сделали вывод результатов теста.");
        }

        // Вывод таблицы рекордов
        Console.WriteLine("\nТаблица рекордов:");
        List<Record> records = RecordTable.GetRecords();
        foreach (Record record in records)
        {
            Console.WriteLine($"{record.Name}: {record.CharsPerMinute} символов в минуту, {record.CharsPerSecond} символов в секунду");
        }

        // Сохранение таблицы рекордов в файл
        RecordTable.SaveLeaderboard();

        Console.WriteLine("\nХотите пройти тест заново? (да/нет)");
        string choice = Console.ReadLine().ToLower();

        if (choice == "да")
        {
            Main(args); // Рекурсивный вызов Main для повторного прохождения теста
        }
    }

    static void InputThread()
    {
        ConsoleKeyInfo keyInfo;

        while (true)
        {
            keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                isTextCompleted = true;
                break;
            }
            else if (keyInfo.Key == ConsoleKey.Backspace)
            {
                if (typedText.Length > 0)
                {
                    typedText = typedText.Remove(typedText.Length - 1);
                }
            }
            else
            {
                typedText += keyInfo.KeyChar;
            }

            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write($"{typedText}");
            CheckTextMatch(typedText);
        }
    }

    static void CheckTextMatch(string inputText)
    {
        string targetText = "весной все на земле пробуждается к новой жизни. растаивает снег. появляется молодая зеленая травка.";
        Console.Clear();
        Console.WriteLine(targetText);
        Console.WriteLine("-----------------------");

        for (int i = 0; i < inputText.Length; i++)
        {
            Console.ForegroundColor = (inputText[i] == targetText[i]) ? ConsoleColor.Blue : ConsoleColor.Red;
            Console.Write(targetText[i]);
        }

        Console.ResetColor();
        Console.WriteLine();
    }
}

class Record
{
    public string Name { get; set; }
    public int CharsPerMinute { get; set; }
    public int CharsPerSecond { get; set; }

    public Record(string name, int charsPerMinute, int charsPerSecond)
    {
        Name = name;
        CharsPerMinute = charsPerMinute;
        CharsPerSecond = charsPerSecond;
    }
}

static class RecordTable
{
    private static List<Record> records = new List<Record>();
    private static string filename = "leaderboard.json";

    public static void AddRecord(Record record)
    {
        LoadLeaderboard();

        records.Add(record);
    }

    public static List<Record> GetRecords()
    {
        return records;
    }

    public static void SaveLeaderboard()
    {
        string json = JsonSerializer.Serialize(records);
        File.WriteAllText(filename, json);
    }

    private static void LoadLeaderboard()
    {
        try
        {
            if (File.Exists(filename))
            {
                string json = File.ReadAllText(filename);
                records = JsonSerializer.Deserialize<List<Record>>(json);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при загрузке таблицы лидеров: " + ex.Message);
        }
    }
}