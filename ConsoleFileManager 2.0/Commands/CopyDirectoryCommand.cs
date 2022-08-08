using ConsoleFileManager_2._0.GUI;
using FileManager.Commands.Base;

namespace FileManager.Commands;

public class CopyDirectoryCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public CopyDirectoryCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
    public override string Description => "Копирование директории";

    private void Copy(string dir_path, string newDir_path, bool recursive)
    {
        var dir = new DirectoryInfo(dir_path);
        if (!dir.Exists)
        {
            return;
        }
        DirectoryInfo[] dirs = dir.GetDirectories();
        Directory.CreateDirectory(newDir_path);
        foreach (FileInfo file in dir.GetFiles())
        {
            string targetFilePath = Path.Combine(newDir_path, file.Name);
            file.CopyTo(targetFilePath);
        }

        if (recursive)
        {
            foreach (DirectoryInfo subDir in dirs)
            {
                string newDestinationDir = Path.Combine(newDir_path, subDir.Name);
                Copy(subDir.FullName, newDestinationDir, true);
            }
        }
        _UserInterface.WriteLine($"Директория {dir_path} скопирована в {newDir_path}");
    }

    public override void Execute(string[] args)
    {
        DirectoryInfo? directory = _FileManager.CurrentDirectory;
        if (args.Length < 3 & Directory.Exists(directory.ToString().Trim()))
            Copy(directory.ToString(), args[1], true);
        else
        {
            if (!Directory.Exists(args[1]))
            {
                var args1 = String.Join(" ", args);
                var fileWithSpace = args1.Split('\"');

                if (!Directory.Exists(fileWithSpace[1]))
                    _UserInterface.WriteLine("Указанной директории не существует.");

                var dir_path = fileWithSpace[1];
                var newDir_path = fileWithSpace[2];

                Copy(dir_path, newDir_path, true);
            }
            else
            {
                var dir_path = args[1];
                var newDir_path = args[2];

                Copy(dir_path, newDir_path, true);
            }
        }
    }
}
