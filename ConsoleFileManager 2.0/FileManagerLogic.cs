using FileManager.Commands;
using FileManager.Commands.Base;

namespace FileManager;

public class FileManagerLogic
{
    private bool _CanWork = true;

    private readonly IUserInterface _UserInterface;

    public DirectoryInfo CurrentDirectory { get; set; } = new("c:\\");

    public IReadOnlyDictionary<string, FileManagerCommand> Commands { get; }

    public FileManagerLogic(IUserInterface UserInterface)
    {
        _UserInterface = UserInterface;

        var list_dir_command = new PrintDirectoryFilesCommand(UserInterface, this);
        var help_command = new HelpCommand(UserInterface, this);
        var quit_command = new QuitCommand(this);
        Commands = new Dictionary<string, FileManagerCommand>
        {
            { "drives", new ListDrivesCommand(UserInterface) },
            { "dir\t", list_dir_command },
            { "ListDir", list_dir_command },
            { "help", help_command },
            { "info", help_command },
            { "quit", quit_command },
            { "exit", quit_command },
            { "cd", new ChangeDirectoryCommand(UserInterface, this) },
            { "RmFile", new DeleteFileCommand(UserInterface, this) },
            { "DelFile", new DeleteFileCommand(UserInterface, this)  },
            { "RmDir", new DeleteDirectoryCommand(UserInterface, this) },
            { "DelDir", new DeleteDirectoryCommand(UserInterface, this)  },
            { "AddFile", new AddFileCommand(UserInterface, this) },
            { "AddDir", new AddDirectoryCommand(UserInterface, this) },
            { "RnFile", new RenameFileCommand(UserInterface, this) },
            { "RenameFile", new RenameFileCommand(UserInterface, this) },
            { "RnDir", new RenameDirectoryCommand(UserInterface, this) },
            { "RenameDir", new RenameDirectoryCommand(UserInterface, this) },
            { "CpFile", new CopyFileCommand(UserInterface, this) },
            { "CopyFile", new CopyFileCommand(UserInterface, this) },
            { "CpDir", new CopyDirectoryCommand(UserInterface, this) },
            { "CopyDir", new CopyDirectoryCommand(UserInterface, this) },

        };

        // рефлексия - для автоматизации добавления команд в словарь
    }


    public void Start()
    {
        _UserInterface.WriteLine("Файловый менеджер v2.0");

        //var can_work = true;
        do
        {
            var input = _UserInterface.ReadLine($"Текущая директория {CurrentDirectory}\nВведите команду:> ", false);

            var args = input.Split(' ');
            var command_name = args[0];

            if (!Commands.TryGetValue(command_name, out var command))
            {
                _UserInterface.WriteLine($"Неизвестная команда {command_name}.");
                _UserInterface.WriteLine("Для справки введите help, для выхода quit");
                continue;
            }

            try
            {
                command.Execute(args);
            }
            catch (Exception error)
            {
                _UserInterface.WriteLine($"При выполнении команды {command_name} произошла ошибка:");
                _UserInterface.WriteLine(error.Message);
            }
        }
        while (_CanWork);


        _UserInterface.WriteLine("Логика файлового менеджера завершена. Всего наилучшего!");
    }

    public void Stop()
    {
        _CanWork = false;
    }
}
