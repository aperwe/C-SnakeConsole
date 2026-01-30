using System;
using System.Threading;

namespace SnakeGame.Core
{
    public class GameEngine
    {
        private readonly int _width;
        private readonly int _height;
        private readonly Snake _snake;
        private readonly Renderer _renderer;
        private Point _food;
        private bool _isRunning = true;
        private Random _rnd = new();

        public GameEngine(int width, int height)
        {
            _width = width;
            _height = height;
            _snake = new Snake(width / 2, height / 2);
            _renderer = new Renderer();
            GenerateFood();
        }

        public void Run()
        {
            _renderer.DrawBoundary(_width, _height);
            _renderer.DrawFood(_food);

            while (_isRunning)
            {
                if (Console.KeyAvailable) HandleInput();
                
                Update();
                Thread.Sleep(100); // Control Game Speed
            }
        }

        private void HandleInput()
        {
            var key = Console.ReadKey(true).Key;
            _snake.CurrentDirection = key switch
            {
                ConsoleKey.UpArrow when _snake.CurrentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when _snake.CurrentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when _snake.CurrentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when _snake.CurrentDirection != Direction.Left => Direction.Right,
                _ => _snake.CurrentDirection
            };
        }

        private void Update()
        {
            bool eating = _snake.Head.X == _food.X && _snake.Head.Y == _food.Y;
            _snake.Move(eating);

            if (eating)
            {
                GenerateFood();
                _renderer.DrawFood(_food);
            }

            // Collisions
            if (_snake.Head.X <= 0 || _snake.Head.X >= _width || 
                _snake.Head.Y <= 0 || _snake.Head.Y >= _height ||
                _snake.Body.Skip(1).Any(p => p == _snake.Head))
            {
                _isRunning = false;
                return;
            }

            _renderer.DrawSnake(_snake);
        }

        private void GenerateFood() => _food = new Point(_rnd.Next(1, _width), _rnd.Next(1, _height));
    }
}