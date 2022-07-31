using FileManager.Commands.Base;

namespace FileManager.Commands;

public class DirectoryRenameCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public DirectoryRenameCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
    public override string Description => "Переименование директории.";

    private void Aprove(string dir_name, string newDirName)
    {
        
        var aprove = _UserInterface.ReadLine($"Переименовать {dir_name} да/нет? ", false).ToLower();
        if (aprove == "да")
        {
            Directory.Move(dir_name, newDirName);
            _UserInterface.WriteLine($"Директория {dir_name} переименована в {newDirName}.");
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
            var dir_name = args[1];
            var newDirName = args[2];

            if (!Directory.Exists(dir_name))
            {
                var args1 = String.Join(" ", args);
                var dirWithSpace = args1.Split('\"');

                if (dirWithSpace.Length < 2 || !Directory.Exists(dirWithSpace[1]))
                    _UserInterface.WriteLine("Указанной директории не существует.");
                                
                Aprove(dirWithSpace[1], dirWithSpace[3]);
            }
            else if (File.Exists(dir_name))
            {
                Aprove(dir_name, newDirName);
            }
        }
    }
}


