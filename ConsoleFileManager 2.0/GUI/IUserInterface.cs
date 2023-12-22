using System.Text;

namespace ConsoleFileManager_2._0.GUI;

public interface IUserInterface
{
    void Write(string str);
    void Write(StringBuilder message, string[] args);

    void WriteLine(string str);

    void WriteLine(object obj);

    string ReadLine(string? Prompt, bool PromptNewLine = true);

    int ReadInt(string? Prompt, bool PromptNewLine = true);

    double ReadDouble(string? Prompt, bool PromptNewLine = true);
}
