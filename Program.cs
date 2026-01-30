using SnakeGame.Core;

Console.CursorVisible = false;
Console.Title = "C# Modular Snake";

var engine = new GameEngine(20, 20);
engine.Run();

Console.SetCursorPosition(0, 21);
Console.WriteLine("Game Over! Press any key to exit...");
Console.ReadKey();