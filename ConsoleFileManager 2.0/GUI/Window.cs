using ConsoleFileManager_2._0.GUI;
using System.Text;

namespace FileManagerOOP.GUI;

public class Window : IUserInterface
{
    public int X { get; }

    public int Y { get; }

    public int Width { get; }

    public int Height { get; }

    public Window(int x, int y, int width, int height)
    {
        this.X = x;
        this.Y = y;
        this.Width = width;
        this.Height = height;
    }

    public void DrawWindow()
    {
        //шапка
            Console.SetCursorPosition(X, Y);
            Console.Write("┌");
            for (int i = 0; i < Width - 2; i++)
                Console.Write("─");
            Console.Write("┐");
            //окно
            Console.SetCursorPosition(X, Y + 1);
            for (int i = 0; i < Height - 2; i++)
            {
                Console.Write("│");
                for (int j = X + 1; j < X + Width - 1; j++)
                    Console.Write(" ");
                Console.Write("│");
            }
            //подвал
            Console.Write("└");
            for (int i = 0; i < Width - 2; i++)
                Console.Write("─");
            Console.Write("┘");
            Console.SetCursorPosition(X, Y);
    }

    public void WriteMesage(string? message, bool newLine)
    {
        //if(message != null && message.Length > 0)
        if (message is { Length: > 0 })
        {
            if (newLine)
            {
                WriteLine(message);
            }
            else
            {
                Write(message);
            }
        }
    }

    public double ReadDouble(string? message, bool newLine = true)
    {
        bool success;
        double result;
        do
        {
            WriteMesage(message, newLine);

            var input = Console.ReadLine();
            success = double.TryParse(input, out result);
            if (!success)
                WriteLine("Строка имела неверный формат");
        }
        while (!success);

        return result;
    }

    public int ReadInt(string? message, bool newLine = true)
    {
        bool success;
        int result;
        do
        {
            WriteMesage(message, newLine);

            var input = Console.ReadLine();
            success = int.TryParse(input, out result);
            if (!success)
                WriteLine("Строка имела неверный формат");
        }
        while (!success);

        return result;
    }

    public virtual string ReadLine(string? message, bool newLine = true) { return ""; }

    public virtual void Write(string message)
    {
        SetCursorPosition();

        Console.Write(message);
    }

    public virtual void WriteLine(string message)
    {
        SetCursorPosition();

        Console.WriteLine(message);
    }

    private void SetCursorPosition()
    {
        (int left, int top) = GetCursorPosition();
        if (top > Y + Height - 2)
        {
            Console.SetCursorPosition(X, Y + 1);
            (left, top) = GetCursorPosition();
        }
        if (left == 0)
            Console.SetCursorPosition(left + 1, top);
    }

    private (int left, int top) GetCursorPosition()
    {
        return (Console.CursorLeft, Console.CursorTop);
    }

    public void Write(StringBuilder tree, string[] args)
    {
        int page;
        int pageLines = Height - 3;
        string[] lines = tree.ToString().Split('\n');
        int pageTotal = (lines.Length + pageLines - 1) / pageLines;

        if (args.Length > 2 && args[1] == "-p" && int.TryParse(args[2], out page))
        {
            if (page > pageTotal)
            {
                page = pageTotal;
            }
        }
        else
        {
            page = 1;
        }

        for (int i = (page - 1) * pageLines, counter = 0; i < page * pageLines; i++, counter++)
        {
            if (lines.Length - 1 > i)
            {
                WriteLine(lines[i]);
            }
        }

        string footer = $"╣ {page} of {pageTotal} ╠";
        Console.SetCursorPosition(Width / 2 - footer.Length / 2, Height - 1);
        Console.Write(footer);
    }

    public void WriteLine(object obj)
    {
        throw new NotImplementedException();
    }
}