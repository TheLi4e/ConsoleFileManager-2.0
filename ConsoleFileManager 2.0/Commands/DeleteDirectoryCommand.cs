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
    /// <param name="dir_path">Путь к директории</param>
    private void Aprove (string dir_path)
    {
        var aprove = _UserInterface.ReadLine($"Вы точно хотите удалить {dir_path} да/нет?", false).ToLower();
        if (aprove == "да")
        {
            Directory.Delete(dir_path, true);
            _UserInterface.WriteLine($"Директория {dir_path} удален.");
        }
        else
            return;
    }
    public override void Execute(string[] args)
    {

        DirectoryInfo? directory = _FileManager.CurrentDirectory;
        if (args.Length < 3)
        {
            Aprove(directory.ToString());
            return;
        }
        else
        {
            var dir_path = args[1];
            if (!Directory.Exists(dir_path))
            {

                var args1 = String.Join(" ", args);
                var dirWithSpace = args1.Split('\"');

                if (dirWithSpace.Length < 2 || !Directory.Exists(dirWithSpace[1]))
                    _UserInterface.WriteLine("Указанная директория не существует.");

                Aprove(dirWithSpace[1]);
            }


            else if (Directory.Exists(dir_path))
            {
                Aprove(dir_path);

            }
        }

    }
}
