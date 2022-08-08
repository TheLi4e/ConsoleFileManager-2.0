using ConsoleFileManager_2._0.GUI;
using FileManager.Commands.Base;
namespace FileManager.Commands;

public class AddDirectoryCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public override string Description => "Для создания вложенной директории введите имя папки.\n│\t\tДля создания папки в другой директории введите путь и имя папки через пробел.";
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
        var aprove = _UserInterface.ReadLine($"Создать директорию {dir_name} да/нет? ", false).ToLower();
        if (aprove == "да")
        {
            Directory.CreateDirectory(toOverride);
            _UserInterface.WriteLine($"Директория {dir_name} создана.");
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
            if (!Directory.Exists(args[1]))
            {
                var args1 = String.Join(" ", args);
                var fileWithSpace = args1.Split('\"');
                var dir_path = fileWithSpace[1];
                var dir_name = fileWithSpace[2];

                if (!Directory.Exists(fileWithSpace[1]))
                    _UserInterface.WriteLine("Указанной директории не существует.");

                Aprove(dir_path, dir_name);
            }
            else
            {
                var dir_path = args[1];
                var file_name = args[2];
                
                Aprove(dir_path, file_name);
            }
        }
       

    }
}
