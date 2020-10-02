using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Drawing;

namespace Assessment.Conway
{
    public struct Board
    {
        private bool[,] board;

        public Size Size { get; }

        public bool this[Point p]
        {
            get => board[p.X, p.Y];
            set => board[p.X, p.Y] = value;
        }

        public bool this[int x, int y]
        {
            get => board[x, y];
            set => board[x, y] = value;
        }

        public Board(Size size)
        {
            this.Size = size;
            this.board = new bool[size.Width, size.Height];
        }

        public Board(bool[,] seed)
        {
            this.Size = new Size(seed.GetLength(0), seed.GetLength(1));
            this.board = seed;
        }

        public void Traverse(Action<Point, bool> action)
        {
            for (int x = 0; x < this.Size.Width; x++)
            {
                for (int y = 0; y < this.Size.Height; y++)
                {
                    action(new Point(x, y), this.board[x, y]);
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if(obj is null || obj.GetType() != typeof(Board))
            {
                return false;
            }

            Board left = this;
            Board right = (Board)obj;
            if (left.Size != right.Size)
            {
                return false;
            }

            for (int x = 0; x < left.Size.Width; x++)
            {
                for (int y = 0; y < left.Size.Height; y++)
                {
                    if (left[x, y] != right[x, y])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return this.board.GetHashCode();
        }
    }
}
