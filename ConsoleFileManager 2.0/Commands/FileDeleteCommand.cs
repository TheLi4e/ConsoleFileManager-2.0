using FileManager.Commands.Base;

namespace FileManager.Commands;

public class FileDeleteCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public override string Description => "Удаление файла";
    public FileDeleteCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
    /// <summary>
    /// Метод для подтверждения
    /// </summary>
    /// <param name="file_path">Путь к файлу</param>
    private void Aprove(string file_path)
    {
        var aprove = _UserInterface.ReadLine($"Вы точно хотите удалить {file_path} да/нет?", false).ToLower();
        if (aprove == "да")
        {
            Directory.Delete(file_path, true);
            _UserInterface.WriteLine($"Файл {file_path} удален.");
        }
        else
            return;
    }

    public override void Execute(string[] args)
    {

        if (args.Length < 2 || string.IsNullOrWhiteSpace(args[1]))
        {
            _UserInterface.WriteLine("Для команды удаления файла необходимо указать один параметр - целевой каталог");
            return;
        }

        var file_path = args[1];
       
        if (!File.Exists(file_path))
        {
            var args1 = String.Join(" ", args);
            var fileWithSpace = args1.Split('\"');

            if (fileWithSpace.Length < 2 || !Directory.Exists(fileWithSpace[1]))
                _UserInterface.WriteLine("Указанная директория не существует.");

            else
                Aprove(fileWithSpace[1]);
        }

        else if (File.Exists(file_path))
        {
            Aprove(file_path);
        }

    }
}
