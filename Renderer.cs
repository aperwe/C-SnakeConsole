using System;

namespace SnakeGame.Core
{
    public class Renderer
    {
        public void DrawBoundary(int width, int height)
        {
            for (int i = 0; i <= width; i++)
            {
                DrawPoint(i, 0, '#');
                DrawPoint(i, height, '#');
            }
            for (int i = 0; i <= height; i++)
            {
                DrawPoint(0, i, '#');
                DrawPoint(width, i, '#');
            }
        }

        public void DrawSnake(Snake snake)
        {
            // Erase old tail
            if (snake.LastTail != null)
                DrawPoint(snake.LastTail.X, snake.LastTail.Y, ' ');

            // Draw new head
            DrawPoint(snake.Head.X, snake.Head.Y, 'O');
        }

        public void DrawFood(Point food) => DrawPoint(food.X, food.Y, '*');

        private void DrawPoint(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
    }
}