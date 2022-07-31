using FileManager.Commands.Base;

namespace FileManager.Commands;

public class ChangeDirectoryCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public override string Description => "\tИзменение текущего каталога";

    public ChangeDirectoryCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }

    public override void Execute(string[] args)
    {
        if (args.Length < 2 || string.IsNullOrWhiteSpace(args[1]))
        {
            _UserInterface.WriteLine("Для команды смены каталога необходимо указать один параметр - целевой каталог");
            return;
        }

        var dir_path = args[1];

        DirectoryInfo? directory;

        if (dir_path == "..")
        {
            directory = _FileManager.CurrentDirectory.Parent;
            if (directory is null)
            {
                _UserInterface.WriteLine("Невозможно подняться выше по дереву каталогов");
                return;
            }
        }
        else if (!Path.IsPathRooted(dir_path))
            dir_path = Path.Combine(_FileManager.CurrentDirectory.FullName, dir_path);
        directory = new DirectoryInfo(dir_path);

        if (!directory.Exists)
        {
            var args1 = String.Join(" ", args);
            var dirWithSpace = args1.Split('\"');
            directory = new DirectoryInfo(dirWithSpace[1]);

            if (!Directory.Exists(dirWithSpace[1]))
                _UserInterface.WriteLine($"Директория {directory} не существует");

            _FileManager.CurrentDirectory = directory;

            _UserInterface.WriteLine($"Текущая директория изменена на {directory.FullName}");
            return;
        }

        _FileManager.CurrentDirectory = directory;

        _UserInterface.WriteLine($"Текущая директория изменена на {directory.FullName}");

        Directory.SetCurrentDirectory(directory.FullName);
    }
}