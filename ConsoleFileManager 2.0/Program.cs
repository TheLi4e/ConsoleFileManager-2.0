using ConsoleFileManager_2._0.GUI;
using FileManager;
using FileManagerOOP.GUI;




const int WINDOW_HEGHT = 50;
const int WINDOW_WIDTH = 120;

Console.BackgroundColor = ConsoleColor.DarkCyan;
Console.ForegroundColor = ConsoleColor.White;

Console.Title = "ConsoleFileManager 2.0";

Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEGHT);
Console.SetBufferSize(WINDOW_WIDTH, WINDOW_HEGHT);

var defaultWindow = new Window(0, 0, WINDOW_WIDTH, 38);
var infoWindow = new InfoWindow(0, 38, WINDOW_WIDTH, 8);
var consoleWindow = new ConsoleWindow(0, 46, 120, 3);

var manager = new FileManagerLogic(defaultWindow, infoWindow, consoleWindow);

defaultWindow.DrawWindow();
infoWindow.DrawWindow();
consoleWindow.DrawWindow();

manager.Start();


manager.Stop();
