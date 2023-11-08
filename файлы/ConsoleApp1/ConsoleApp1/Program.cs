using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

public class Figure
{
    public string Name { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}

class Program
{
    private static List<Figure> figures = new List<Figure>();
    private static string filePath;

    static void Main(string[] args)
    {
        Console.WriteLine("Добро пожаловать в текстовый редактор!");
        Console.Write("Введите путь к файлу: ");
        filePath = Console.ReadLine();

        LoadFile();
        PrintFigures();
        Console.WriteLine("Нажмите F1 для сохранения файла, Escape для выхода.");
        while (true)
        {
            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.F1)
            {
                SaveFile();
            }
            else if (key == ConsoleKey.Escape)
            {
                break;
            }
        }
    }
    private static void PrintFigures()
    {
        Console.WriteLine("Данные из загруженного файла:");
        foreach (var figure in figures)
        {
            Console.WriteLine($"Имя: {figure.Name}");
            Console.WriteLine($"Ширина: {figure.Width}");
            Console.WriteLine($"Высота: {figure.Height}");
            Console.WriteLine();
        }
    }
    private static void LoadFile()
    {
        if (File.Exists(filePath))
        {
            string fileExtension = Path.GetExtension(filePath);
            switch (fileExtension)
            {
                case ".txt":
                    string[] lines = File.ReadAllLines(filePath);
                    for (int i = 0; i < lines.Length; i += 3)
                    {
                        if (i + 2 < lines.Length)
                        {
                            Figure figure = new Figure
                            {
                                Name = lines[i],
                                Width = Convert.ToDouble(lines[i + 1]),
                                Height = Convert.ToDouble(lines[i + 2])
                            };
                            figures.Add(figure);
                        }
                    }
                    break;
                case ".json":
                    string jsonContent = File.ReadAllText(filePath);
                    figures = JsonConvert.DeserializeObject<List<Figure>>(jsonContent);
                    break;
                case ".xml":
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Figure>));
                    using (TextReader textReader = new StreamReader(filePath))
                    {
                        figures = (List<Figure>)serializer.Deserialize(textReader);
                    }
                    break;
                default:
                    Console.WriteLine("Неподдерживаемый формат файла.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Файл не существует. Будет создан новый файл.");
        }
    }

    private static void SaveFile()
    {
        Console.WriteLine("Введите путь и имя файла для сохранения (например, C:\\новый_файл.txt): ");
        string savePath = Console.ReadLine();
        string saveExtension = Path.GetExtension(savePath);

        using (StreamWriter writer = new StreamWriter(savePath))
        {
            switch (saveExtension)
            {
                case ".txt":
                    foreach (var figure in figures)
                    {
                        writer.WriteLine(figure.Name);
                        writer.WriteLine(figure.Width);
                        writer.WriteLine(figure.Height);
                    }
                    break;
                case ".json":
                    string jsonContent = JsonConvert.SerializeObject(figures, Formatting.Indented);
                    writer.Write(jsonContent);
                    break;
                case ".xml":
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Figure>));
                    serializer.Serialize(writer, figures);
                    break;
                default:
                    Console.WriteLine("Неподдерживаемый формат сохранения файла.");
                    break;
            }
        }

        Console.WriteLine("Файл успешно сохранен(Оцени нашу работу и не дай подзатыльник создателю)");
    }
}