using ConsoleFileManager_2._0.GUI;
using FileManager.Commands.Base;

namespace FileManager.Commands;

public class MoveDirCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public MoveDirCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
    public override string Description => "Перемещение директории";

    private void Move(string dir_path, string newDir_path)
    {
        Directory.Move(dir_path, newDir_path);
        _UserInterface.WriteLine($"Директория перемещена в {newDir_path}");

    }

    public override void Execute(string[] args)
    {
        DirectoryInfo? directory = _FileManager.CurrentDirectory;
        if (args.Length < 3 & Directory.Exists(directory.ToString().Trim()))
            Move(directory.ToString(), args[1]);
        else
        {
            if (!Directory.Exists(args[1]))
            {
                var args1 = String.Join(" ", args);
                var fileWithSpace = args1.Split('\"');

                if (!Directory.Exists(fileWithSpace[1]))
                    _UserInterface.WriteLine("Указанной директории не существует.");

                var dir_path = fileWithSpace[1];
                var newDir_path = fileWithSpace[3];

                Move (dir_path, newDir_path);
            }
            else
            {
                var dir_path = args[1];
                var newDir_path = args[2];

                Move(dir_path, newDir_path);
            }
        }
    }
}
