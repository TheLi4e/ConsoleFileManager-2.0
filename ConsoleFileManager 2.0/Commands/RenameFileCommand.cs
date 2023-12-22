using ConsoleFileManager_2._0.GUI;
using FileManager.Commands.Base;

namespace FileManager.Commands;

public class RenameFileCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;
    public RenameFileCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }
    public override string Description => "Переименование файла. Для переименования файла в текущей директории введите текущее имя файла\n│\t\tи новое имя файла через пробел. Для переименовании файла в другой директории\n│\t\tвведите сначала путь. ";

    private void Aprove(string dir_path, string file_name, string newFileName)
    {

        var toRename = dir_path.Trim() + file_name.Trim();
        var newName = dir_path.Trim() + newFileName.Trim();
        var aprove = _UserInterface.ReadLine($"Переименовать {file_name} да/нет? ", false).ToLower();
        if (aprove == "да")
        {
            Directory.Move(toRename, newName);
            _UserInterface.WriteLine($"Файл {file_name} переименован в {newFileName}.");
        }
        else
            return;
    }
    public override void Execute(string[] args)
    {
        DirectoryInfo? directory = _FileManager.CurrentDirectory;
        if (args.Length < 4 & File.Exists(directory.ToString().Trim() + args[1].Trim()))
            Aprove(directory.ToString(), args[1], args[2]);
        else if (args.Length < 4 & !File.Exists(directory.ToString().Trim() + args[1].Trim()))
            _UserInterface.WriteLine("Указанный файл не существует.");
        else
        {
            var dir_path = args[1];
            var file_name = args[2];
            var newFileName = args[3];
            var toRename = dir_path.Trim() + file_name.Trim();

            if (!File.Exists(toRename))
            {
                _UserInterface.WriteLine("Указанный файл не существует.");
            }
            else if (File.Exists(toRename))
            {
                Aprove(dir_path, file_name, newFileName);
            }
        }
    }
}


