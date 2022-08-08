using ConsoleFileManager_2._0.GUI;
using FileManager.Commands.Base;
using System.Text;

namespace FileManager.Commands;

public class PrintDirectoryFilesCommand : FileManagerCommand
{
    private readonly IUserInterface _UserInterface;
    private readonly FileManagerLogic _FileManager;

    public override string Description => "Вывод содержимого директории. Для вывода конкретной страницы укажите аргумент -p N,\n│\t\tгде N - номер страницы";

    public PrintDirectoryFilesCommand(IUserInterface UserInterface, FileManagerLogic FileManager)
    {
        _UserInterface = UserInterface;
        _FileManager = FileManager;
    }

    public override void Execute(string[] args)
    {
        var directory = _FileManager.CurrentDirectory;

        StringBuilder tree = new StringBuilder();
        tree.Append($"{directory}\n");

        DirectoryInfo[] sub_dirs = directory.GetDirectories();
        for (int i = 0; i < sub_dirs.Length; i++)
        {
            if (i == sub_dirs.Length - 1)
                tree.Append($"  └─{sub_dirs[i].Name}\n");
            else
                tree.Append($"  ├─{sub_dirs[i].Name}\n");
        }

        FileInfo[] files = directory.GetFiles();
        long total_length = 0;

        for (int i = 0; i < files.Length; i++)
        {
            if (i == files.Length - 1)
            {
                tree.Append($"  └─{files[i].Name}\n");
            }
            else
            {
                tree.Append($"  ├─{files[i].Name}\n");
            }
            total_length += files[i].Length;
        }
        _UserInterface.Write(tree, args);
    }
}
