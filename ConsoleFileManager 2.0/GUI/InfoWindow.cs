namespace FileManagerOOP.GUI;

public class InfoWindow : Window
{
    public const int _consoleHeight = 8;

    public InfoWindow(int positionX, int positionY, int width, int height) : base(positionX, positionY, width, _consoleHeight)
    {
    }

    public override string ReadLine(string? message, bool newLine = true)
    {
        Write(message!);
        return Console.ReadLine()!;
    }

    public override void Write(string message)
    {
        SetCursorPosition();

        Console.Write(message);
    }

    public override void WriteLine(string message)
    {
        SetCursorPosition();

        Console.WriteLine(message);
    }

    private void SetCursorPosition()
    {
        (int left, int top) = GetCursorPosition();
        if (top > Y + Height - 1 || top < Y)
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
}