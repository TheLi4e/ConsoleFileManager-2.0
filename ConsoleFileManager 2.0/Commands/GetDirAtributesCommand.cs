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

    /// <summary>
    /// Вывод информации о директории
    /// </summary>
    /// <param name="dir">Указанная директория</param>
   private void GetInfo(DirectoryInfo dir)
    {
        _UserInterface.Write ("Сводные данные:\t\t\t");
        SubDirInfo(dir);
        _UserInterface.WriteLine($"Аттрибуты директории {dir.Name}: \t\t{dir.Attributes}");
        _UserInterface.WriteLine($"Время создания директории: \t\t{dir.CreationTime}");
        _UserInterface.WriteLine($"Время последнего доступа к каталогу: \t{dir.LastAccessTime}");
        _UserInterface.WriteLine($"Путь к директории: \t\t\t{dir.FullName}");
    }

    /// <summary>
    /// Получение данных о вложенных папках и файлах
    /// </summary>
    /// <param name="dir">Указанная директория</param>
    private void SubDirInfo (DirectoryInfo dir)
    {
        var dirs_count = dir.EnumerateDirectories().Count(); // LINQ

        var files_count = 0;
        long total_length = 0;
        foreach (var file in dir.EnumerateFiles())
        {
            files_count++;
            total_length += file.Length;
        }

        _UserInterface.WriteLine($"Директорий {dirs_count}, файлов {files_count}, (суммарный размер {total_length} байт)");
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
