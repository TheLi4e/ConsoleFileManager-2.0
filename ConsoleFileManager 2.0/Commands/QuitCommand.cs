﻿using FileManager.Commands.Base;

namespace FileManager.Commands;

public class QuitCommand : FileManagerCommand
{
    private readonly FileManagerLogic _FileManager;

    public override string Description => "Выход";

    public QuitCommand(FileManagerLogic FileManager) => _FileManager = FileManager;

    public override void Execute(string[] args)
    {
        _FileManager.Stop();
    }
}
