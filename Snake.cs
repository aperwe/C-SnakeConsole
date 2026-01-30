using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.Core
{
    public enum Direction { Up, Down, Left, Right }
    public record Point(int X, int Y);

    public class Snake
    {
        public LinkedList<Point> Body { get; private set; } = new();
        public Direction CurrentDirection { get; set; } = Direction.Right;
        public Point Head => Body.First.Value;
        public Point LastTail { get; private set; }

        public Snake(int startX, int startY)
        {
            Body.AddFirst(new Point(startX, startY));
            Body.AddLast(new Point(startX - 1, startY));
            Body.AddLast(new Point(startX - 2, startY));
        }

        public void Move(bool grow)
        {
            Point newHead = CurrentDirection switch
            {
                Direction.Up => Head with { Y = Head.Y - 1 },
                Direction.Down => Head with { Y = Head.Y + 1 },
                Direction.Left => Head with { X = Head.X - 1 },
                Direction.Right => Head with { X = Head.X + 1 },
                _ => Head
            };

            Body.AddFirst(newHead);
            if (!grow)
            {
                LastTail = Body.Last.Value;
                Body.RemoveLast();
            }
            else
            {
                LastTail = null; // No tail to erase if we grew
            }
        }
    }
}