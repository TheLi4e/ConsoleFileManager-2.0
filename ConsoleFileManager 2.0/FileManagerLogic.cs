using ConsoleFileManager_2._0.GUI;
using FileManager.Commands;
using FileManager.Commands.Base;
using FileManagerOOP.GUI;

namespace FileManager;

public class FileManagerLogic
{
    private bool _CanWork = true;

    private readonly IUserInterface _DefaultWindow;
    private readonly IUserInterface _InfoWindow;
    private readonly IUserInterface _ConsoleWindow;

    public DirectoryInfo CurrentDirectory { get; set; } = new("c:\\");

    public IReadOnlyDictionary<string, FileManagerCommand> Commands { get; }

    public FileManagerLogic(IUserInterface defaultWindow, IUserInterface infoWindow, IUserInterface consoleWindow)
    {
        _DefaultWindow = defaultWindow;
        _InfoWindow = infoWindow;
        _ConsoleWindow = consoleWindow;

        
        var list_dir_command = new PrintDirectoryFilesCommand(defaultWindow, this);
        var help_command = new HelpCommand(defaultWindow, this);
        var quit_command = new QuitCommand(this);
        Commands = new Dictionary<string, FileManagerCommand>
        {
            { "drives", new ListDrivesCommand(defaultWindow) },
            { "dir", list_dir_command },
            { "ListDir", list_dir_command },
            { "help", help_command },
            { "info", help_command },
            { "quit", quit_command },
            { "exit", quit_command },
            { "cd", new ChangeDirectoryCommand(defaultWindow, this) },
            { "RmFile", new DeleteFileCommand(defaultWindow, this) },
            { "DelFile", new DeleteFileCommand(defaultWindow, this)  },
            { "RmDir", new DeleteDirectoryCommand(defaultWindow, this) },
            { "DelDir", new DeleteDirectoryCommand(defaultWindow, this)  },
            { "AddFile", new AddFileCommand(defaultWindow, this) },
            { "AddDir", new AddDirectoryCommand(defaultWindow, this) },
            { "RnFile", new RenameFileCommand(defaultWindow, this) },
            { "RenameFile", new RenameFileCommand(defaultWindow, this) },
            { "RnDir", new RenameDirectoryCommand(defaultWindow, this) },
            { "RenameDir", new RenameDirectoryCommand(defaultWindow, this) },
            { "CpFile", new CopyFileCommand(defaultWindow, this) },
            { "CopyFile", new CopyFileCommand(defaultWindow, this) },
            { "CpDir", new CopyDirectoryCommand(defaultWindow, this) },
            { "CopyDir", new CopyDirectoryCommand(defaultWindow, this) },
            { "MvFile", new MoveFileCommand(defaultWindow, this) },
            { "MoveFile", new MoveFileCommand(defaultWindow, this) },
            { "MvDir", new MoveDirCommand(defaultWindow, this) },
            { "MoveDir", new MoveDirCommand(defaultWindow, this) },
            { "FileInfo", new GetFileAtributesCommand(defaultWindow, this) },
            { "DirInfo", new GetDirAtributesCommand(defaultWindow, this) },

        };

        // рефлексия - для автоматизации добавления команд в словарь
    }


    public void Start()
    {
        _InfoWindow.WriteLine("Для работы с консольным менеджером введите go.");
             
        _InfoWindow.Write("Чтобы завершить работу программу введите exit: ");
        string TaskNumber = Console.ReadLine();
        switch (TaskNumber)
        {
            case "go":
                do
                {
                    
                    DrawConsole(0, 38, 120, 8);
                    DrawConsole(0, 46, 120, 3);
                  
                    _InfoWindow.Write($"Текущая директория {CurrentDirectory}");
                    var input = _ConsoleWindow.ReadLine($"Введите команду:> ", false);

                    var args = input.Split(' ');
                    var command_name = args[0];

                    if (!Commands.TryGetValue(command_name, out var command))
                    {
                        _InfoWindow.WriteLine($"Неизвестная команда {command_name}.");
                        _InfoWindow.WriteLine("Для справки введите help.");
                        Console.ReadKey(true);
                        continue;
                    }

                    try
                    {
                        command.Execute(args);
                        Console.ReadKey(true);
                        DrawConsole(0, 0, 120, 38);
                    }
                    catch (Exception error)
                    {
                        var infoWindow = new Window(0, 38, 120, 8);
                        infoWindow.DrawWindow();
                        Console.SetCursorPosition(1, 39);
                        _InfoWindow.WriteLine($"При выполнении команды {command_name} произошла ошибка:");
                        _InfoWindow.WriteLine(error.Message);
                        DateTime date = DateTime.Now;
                        if (!File.Exists($@"{Environment.CurrentDirectory}\ErrorsLog.txt"))
                        {
                            File.Create($@"{Environment.CurrentDirectory}\ErrorsLog.txt");
                        }
                        File.AppendAllText($@"{Environment.CurrentDirectory}\ErrorsLog", ($"{date} {error.Message} \n"));
                        Console.ReadKey(true);
                        continue;
                    }
                    
                }
                while (_CanWork);
                break;
            case "exit":
                _CanWork=false;
                DrawConsole(0, 38, 120, 8);
                Console.SetCursorPosition(2, 39);
                _InfoWindow.WriteLine("Завершение работы приложения...");
                Console.ReadKey(true);
                Stop();
                break;
            default:
                DrawConsole(0, 38, 120, 8);
                Console.SetCursorPosition(2, 39);
                _InfoWindow.WriteLine("Ошибка ввода. Запускаю приложение.");
                Console.ReadKey(true);
                goto case "go";
        }
        
    }

    public void Stop()
    {
        _CanWork = false;
    }

    static void DrawConsole(int x, int y, int width, int height)
    {
        var defaultWindow = new Window (x, y, width, height);
        defaultWindow.DrawWindow();
        Console.SetCursorPosition(x + 1, y + height / 2);
    }
}
