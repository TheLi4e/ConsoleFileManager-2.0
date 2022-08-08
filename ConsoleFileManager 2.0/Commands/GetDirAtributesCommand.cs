using ConsoleFileManager_2._0.GUI;
using FileManager.Commands.Base;

namespace FileManager.Commands;

public class GetDirAtributesCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public GetDirAtributesCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
    public override string Description => "Получение атрибутов директории.";


    private void GetInfo(DirectoryInfo dir)
    {
        _UserInterface.WriteLine($"Аттрибуты директории {dir.Name}: \t\t{dir.Attributes}");
        _UserInterface.WriteLine($"Время создания директории: \t\t{dir.CreationTime}");
        _UserInterface.WriteLine($"Время последнего доступа к каталогу: \t{dir.LastAccessTime}");
        _UserInterface.WriteLine($"Путь к директории: \t\t\t{dir.FullName}");
    }

    public override void Execute(string[] args)
    {
        DirectoryInfo? directory = _FileManager.CurrentDirectory;
        if (args.Length < 2 & Directory.Exists(directory.FullName))
            GetInfo(directory);
        else
        {
            if (!Directory.Exists(args[1]))
            {
                var args1 = String.Join(" ", args);
                var fileWithSpace = args1.Split('\"');

                if (!Directory.Exists(fileWithSpace[1]))
                    _UserInterface.WriteLine("Указанной директории не существует.");

                var dir_path = fileWithSpace[1];
                DirectoryInfo dir = new(dir_path);
                GetInfo(dir);
            }
            else
            {
                var dir_path = args[1];
                DirectoryInfo dir = new(dir_path);
                GetInfo(dir);
            }
        }


    }
}
