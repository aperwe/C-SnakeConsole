using SnakeGame.Core;

Console.CursorVisible = false;
Console.Title = "C# Modular Snake";
Console.Clear();

// Defaults
const int DefaultWindowWidth = 40;
const int DefaultWindowHeight = 40;
const int MinWindowSize = 10;

int windowWidth = DefaultWindowWidth;
int windowHeight = DefaultWindowHeight;

// Parse command-line args: expected: <width> <height>
if (args.Length >= 2)
{
    if (!int.TryParse(args[0], out windowWidth) || !int.TryParse(args[1], out windowHeight))
    {
        Console.WriteLine("Invalid size arguments. Using defaults.");
        windowWidth = DefaultWindowWidth;
        windowHeight = DefaultWindowHeight;
    }
}

// Clamp to sensible bounds and the console's largest supported window
int maxW = Math.Max(Console.LargestWindowWidth, MinWindowSize);
int maxH = Math.Max(Console.LargestWindowHeight, MinWindowSize);

windowWidth = Math.Clamp(windowWidth, MinWindowSize, maxW);
windowHeight = Math.Clamp(windowHeight, MinWindowSize, maxH);

// Ensure buffer is at least as large as the window (some consoles require buffer >= window)
try
{
    int bufferWidth = Math.Max(Console.BufferWidth, windowWidth);
    int bufferHeight = Math.Max(Console.BufferHeight, windowHeight + 1); // +1 for messages
    Console.SetBufferSize(bufferWidth, bufferHeight);

    // Set the console window size to the requested values (may throw if unsupported)
    Console.SetWindowSize(windowWidth, windowHeight);
}
catch
{
    // If resizing the console fails (e.g., remote session or terminal), ignore and continue.
    // The engine will still run using the logical width/height values below.
}

// Note about coordinate mapping:
// GameEngine draws a boundary from 0..width and 0..height inclusive, so it needs
// logical game width/height = windowWidth - 1, windowHeight - 1 to fit inside the window.
int gameWidth = Math.Max(1, windowWidth - 1);
int gameHeight = Math.Max(1, windowHeight - 1);

var engine = new GameEngine(gameWidth, gameHeight);
engine.Run();

// Place the Game Over message just below the game area if possible.
int messageRow = Math.Clamp(windowHeight - 1, 0, Console.BufferHeight - 1);
Console.SetCursorPosition(0, messageRow);
Console.WriteLine("Game Over! Press any key to exit...");
Console.ReadKey();