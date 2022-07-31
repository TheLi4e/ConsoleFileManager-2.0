using FileManager.Commands.Base;

namespace FileManager.Commands;

public class CopyFileCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public CopyFileCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
    public override string Description => "Копирование файла";

    private void Copy(string dir_path, string file_name, string newDir_path)
    {
        var toCopy = dir_path.Trim() + file_name.Trim();
        var newDir = newDir_path.Trim() + file_name.Trim();
        if (File.Exists(newDir))
        {
           var aprove = _UserInterface.ReadLine("Файл уже существует. Перезаписать? да/нет").ToLower();
            if (aprove == "да")
            {
                File.Copy(toCopy, newDir, true);
                _UserInterface.WriteLine($"Файл {file_name} скопирован в {newDir_path}.");
            }
            else
                return;
        }

        File.Copy(toCopy, newDir);
        _UserInterface.WriteLine($"Файл {file_name} скопирован в {newDir_path}.");
    }

    public override void Execute(string[] args)
    {
        DirectoryInfo? directory = _FileManager.CurrentDirectory;
        if (args.Length < 4 & File.Exists(directory.ToString().Trim() + args[1].Trim()))
            Copy(directory.ToString(), args[1], args[2]);
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
                Copy(dir_path, file_name, newDir_path);
            }
        }
    }
}
