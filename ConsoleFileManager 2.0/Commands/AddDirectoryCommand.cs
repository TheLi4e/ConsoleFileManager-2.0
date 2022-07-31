using FileManager.Commands.Base;
namespace FileManager.Commands;

public class AddDirectoryCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public override string Description => "Для создания вложенной директории введите имя папки.\n \t\tДля создания папки в другой директории введите путь и имя папки через пробел.";
    public AddDirectoryCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
    /// <summary>
    /// Метод для подтверждения
    /// </summary>
    /// <param name="dir_path">Путь к файлу</param>
    private void Aprove(string dir_path, string dir_name)
    {
        var toOverride = dir_path.Trim() + dir_name.Trim();
        var aprove = _UserInterface.ReadLine($"Создать папку {dir_name} да/нет? ", false).ToLower();
        if (aprove == "да")
        {
            Directory.CreateDirectory(toOverride);
            _UserInterface.WriteLine($"Папка {dir_name} создана.");
        }
        else
            return;
    }

    private void Override(string dir_path, string dir_name)
    {
        var toOverride = dir_path.Trim() + dir_name.Trim();
        var aprove = _UserInterface.ReadLine($"Директория {dir_name} существует. Перезаписать? да/нет? ", false).ToLower();
        if (aprove == "да")
        {
            Directory.CreateDirectory(toOverride);
            _UserInterface.WriteLine($"Директория {dir_name} перезаписана.");
        }
        else
            return;
    }


    public override void Execute(string[] args)
    {

        DirectoryInfo? directory = _FileManager.CurrentDirectory;
        if (args.Length < 3)
            Aprove(directory.ToString(), args[1]);
        else
        {
            var dir_path = args[1];
            var dir_name = args[2];
            var toOverride = dir_path.Trim() + dir_name.Trim();

            if (!Directory.Exists(toOverride))
            {
                dir_path = Path.Combine(_FileManager.CurrentDirectory.FullName, dir_path);
                directory = new DirectoryInfo(dir_path);
                Aprove(dir_path, dir_name);
            }

            else if (Directory.Exists(toOverride))
            {
                Override(dir_path, dir_name);
            }
        }
    }
}
