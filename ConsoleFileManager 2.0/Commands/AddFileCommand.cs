using FileManager.Commands.Base;
namespace FileManager.Commands;

public class AddFileCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public override string Description => "Для создания файла в текущей директории введите имя файла.\n \t\tДля создания файла в другой директории введите путь и имя файла через пробел.";
    public AddFileCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
    /// <summary>
    /// Метод для подтверждения
    /// </summary>
    /// <param name="dir_path">Путь к файлу</param>
    private void Aprove(string dir_path, string file_name)
    {
        var toOverride = dir_path.Trim() + file_name.Trim();
        var aprove = _UserInterface.ReadLine($"Создать файл {file_name} да/нет? ", false).ToLower();
        if (aprove == "да")
        {
            File.Create(toOverride);
            _UserInterface.WriteLine($"Файл {file_name} создан.");
        }
        else
            return;
    }

    private void Override(string dir_path, string file_name)
    {
        var toOverride = dir_path.Trim() + file_name.Trim();
        var aprove = _UserInterface.ReadLine($"Файл {file_name} существует. Перезаписать? да/нет? ", false).ToLower();
        if (aprove == "да")
        {
            File.Create(toOverride);
            _UserInterface.WriteLine($"Файл {file_name} перезаписан.");
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
                var file_name = fileWithSpace[2];
                var toOverride = dir_path.Trim() + file_name.Trim();
                if (!Directory.Exists(fileWithSpace[1]))
                    _UserInterface.WriteLine("Указанной директории не существует.");
                if (!File.Exists(toOverride))
                {
                    Aprove(dir_path, file_name);
                }

                else if (File.Exists(toOverride))
                {
                    Override(dir_path, file_name);
                }
            }

            else
            {
                var dir_path = args[1];
                var file_name = args[2];
                var toOverride = dir_path.Trim() + file_name.Trim();

                if (!File.Exists(toOverride))
                {
                    Aprove(dir_path, file_name);
                }

                else if (File.Exists(toOverride))
                {
                    Override(dir_path, file_name);
                }
            }
        }
    }
}
