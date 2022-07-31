using FileManager.Commands.Base;


namespace FileManager.Commands;

public class DeleteDirectoryCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public override string Description => "Удаление директории";
    public DeleteDirectoryCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
    /// <summary>
    /// Метод для подтверждения
    /// </summary>
    /// <param name="dir_path">Путь к файлу</param>
    private void Aprove (string dir_path)
    {
        var aprove = _UserInterface.ReadLine($"Вы точно хотите удалить {dir_path} да/нет?", false).ToLower();
        if (aprove == "да")
        {
            Directory.Delete(dir_path, true);
            _UserInterface.WriteLine($"Файл {dir_path} удален.");
        }
        else
            return;
    }
    public override void Execute(string[] args)
    {

        if (args.Length < 2 || string.IsNullOrWhiteSpace(args[1]))
        {
            _UserInterface.WriteLine("Для команды удаления директории необходимо указать один параметр - целевой каталог");
            return;
        }

        var dir_path = args[1];
        if (!Directory.Exists(dir_path))
        {
            
            var args1 = String.Join(" ", args);
            var dirWithSpace = args1.Split('\"');
           
            if (dirWithSpace.Length<2 || !Directory.Exists(dirWithSpace[1]))
                _UserInterface.WriteLine("Указанная директория не существует.");

            Aprove(dirWithSpace[1]);
        }


        else if (Directory.Exists(dir_path))
        {
            Aprove(dir_path);

        }

    }
}
