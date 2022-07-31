using FileManager.Commands.Base;

namespace FileManager.Commands;

public class MoveFileCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public override string Description => "Удаление файла";
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
        else if (args.Length < 4 & !File.Exists(directory.ToString().Trim() + args[1].Trim()))
            _UserInterface.WriteLine("Указанный файл не существует.");
        else
        {
            var dir_path = args[1];
            var file_name = args[2];
            var newDir_path = args[3];
            var toRename = dir_path.Trim() + file_name.Trim();

            if (!File.Exists(toRename))
            {
                _UserInterface.WriteLine("Указанный файл не существует.");
            }
            else if (File.Exists(toRename))
            {
                //Copy(dir_path, file_name, newDir_path);
            }
        }

    }
}
