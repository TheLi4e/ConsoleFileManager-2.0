using ConsoleFileManager_2._0.GUI;
using FileManager.Commands.Base;

namespace FileManager.Commands;

public class GetFileAtributesCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public GetFileAtributesCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
    public override string Description => "Получение атрибутов файла.";



    /// <summary>
    /// Метод для вывода информации  файле
    /// </summary>
    /// <param name = "file" > Указанный файл</param>
    private void GetInfo(FileInfo file)
    {
        _UserInterface.WriteLine($"Аттрибуты файла {file.Name}: \t\t {file.Attributes}");
        _UserInterface.WriteLine($"Время создания файла: \t\t\t {file.CreationTime}");
        _UserInterface.WriteLine($"Время последнего доступа к файлу:\t {file.LastAccessTime}");
        _UserInterface.WriteLine($"Размер файла в байтах:\t\t\t {file.Length}");

    }
   
    public override void Execute(string[] args)
    {
        DirectoryInfo? directory = _FileManager.CurrentDirectory;
        FileInfo? file = new FileInfo(directory.FullName);
        if (args.Length < 2 & File.Exists(file.FullName))
            GetInfo(file);
        else
        {
            if (!File.Exists(args[1]))
            {
                var args1 = String.Join(" ", args);
                var fileWithSpace = args1.Split('\"');

                if (!File.Exists(fileWithSpace[1]))
                    _UserInterface.WriteLine("Указанного файла не существует.");

                var dir_path = fileWithSpace[1];
                FileInfo dir = new(dir_path);
                GetInfo(dir);
            }
            else
            {
                var dir_path = args[1];
                FileInfo dir = new(dir_path);
                GetInfo(dir);
            }
        }


    }
}
