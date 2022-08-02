using FileManager.Commands.Base;

namespace FileManager.Commands;

public class MoveFileCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public override string Description => "Перемещение файла";
    public MoveFileCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
     
    public override void Execute(string[] args)
    {

        DirectoryInfo? directory = _FileManager.CurrentDirectory;
        if (args.Length < 4 & File.Exists(directory.ToString().Trim() + args[1].Trim()))
        {
            var moveTo = args[1].Trim() + args[2].Trim();
            File.Move(directory.ToString(), moveTo);
        }
        else
        {
            if (!Directory.Exists(args[1]))
            {
                var args1 = String.Join(" ", args);
                var fileWithSpace = args1.Split('\"');

                if (!Directory.Exists(fileWithSpace[1]))
                    _UserInterface.WriteLine("Указанной директории не существует.");

                var dir_path = fileWithSpace[1];
                var file_name = fileWithSpace[2];
                var newDir_path = fileWithSpace[3];
                var toMove = dir_path.Trim() + file_name.Trim();
                var moveTo = newDir_path.Trim() + file_name.Trim();

                if (!File.Exists(toMove))
                {
                    _UserInterface.WriteLine("Указанный файл не существует.");
                }
                else if (File.Exists(toMove))
                {
                    File.Move(toMove, moveTo);

                    _UserInterface.WriteLine($"Файл {file_name} перемещени в {moveTo}");
                }
            }
            else
            {
                var dir_path = args[1];
                var file_name = args[2];
                var newDir_path = args[3];
                var toMove = dir_path.Trim() + file_name.Trim();
                var moveTo = newDir_path.Trim() + file_name.Trim();
                File.Move(toMove, moveTo);
                _UserInterface.WriteLine($"Файл {file_name} перемещени в {moveTo}");
            }
        }

    }
}
