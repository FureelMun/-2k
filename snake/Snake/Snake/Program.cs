using System;
using System.Collections.Generic;
using System.Threading;

enum Border
{
    MaxRight = 20,
    MaxBottom = 10
}

class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

class Snake
{
    private List<Position> body;
    private Position direction;
    private Position food;
    private bool gameOver;

    public Snake()
    {
        body = new List<Position>();
        body.Add(new Position(10, 5));
        direction = new Position(1, 0);
        gameOver = false;
        GenerateFood();
    }

    private void DrawSnake()
    {
        Console.Clear();

        // Отрисовка вертикальных границ
        for (int i = 0; i <= (int)Border.MaxBottom; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("|");
            Console.SetCursorPosition((int)Border.MaxRight, i);
            Console.Write("|");
        }

        // Отрисовка горизонтальных границ
        for (int i = 0; i <= (int)Border.MaxRight; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("-");
            Console.SetCursorPosition(i, (int)Border.MaxBottom);
            Console.Write("-");
        }

        foreach (Position pos in body)
        {
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.Write("@");
        }

        Console.SetCursorPosition(food.X, food.Y);
        Console.Write("*");
    }

    private void MoveSnake()
    {
        Position head = body[body.Count - 1];
        Position newHead = new Position(head.X + direction.X, head.Y + direction.Y);

        if (newHead.X <= 0 || newHead.X >= (int)Border.MaxRight || newHead.Y <= 0 || newHead.Y >= (int)Border.MaxBottom)
        {
            gameOver = true;
            return;
        }

        foreach (Position pos in body)
        {
            if (pos.X == newHead.X && pos.Y == newHead.Y)
            {
                gameOver = true;
                return;
            }
        }

        body.Add(newHead);
        if (newHead.X == food.X && newHead.Y == food.Y)
        {
            GenerateFood();
        }
        else
        {
            body.RemoveAt(0);
        }
    }

    private void GenerateFood()
    {
        Random random = new Random();
        int x = random.Next(1, (int)Border.MaxRight);
        int y = random.Next(1, (int)Border.MaxBottom);
        food = new Position(x, y);
    }

    public void StartGame()
    {
        while (!gameOver)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        direction = new Position(-1, 0);
                        break;
                    case ConsoleKey.RightArrow:
                        direction = new Position(1, 0);
                        break;
                    case ConsoleKey.UpArrow:
                        direction = new Position(0, -1);
                        break;
                    case ConsoleKey.DownArrow:
                        direction = new Position(0, 1);
                        break;
                }
            }

            MoveSnake();
            DrawSnake();
            Thread.Sleep(100);
        }

        Console.SetCursorPosition(0, (int)Border.MaxBottom + 2);
        Console.WriteLine("Game over!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Snake snake = new Snake();
        snake.StartGame();
    }
}

// В коде при изменении символа границ на * очень сильно рябит в глаза, проверино XD
// Для удобства в значения размеров лучше брать 2 к 1. Здесь 20 на 10 к примеру
// Игра очень залипательная, поиграла в нее часик и ввспомнила про зачеты XD