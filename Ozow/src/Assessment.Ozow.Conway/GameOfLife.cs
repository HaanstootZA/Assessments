using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Text;

namespace Assessment.Ozow.Conway
{
    public class GameOfLife : IEnumerable<bool[,]>
    {
        private BoardEnumerator boardEnumerator;

        public GameOfLife(bool[,] seedBoard, int maximumGenerations)
        {
            if (seedBoard is null)
            {
                throw new ArgumentNullException(nameof(seedBoard));
            }

            this.boardEnumerator = new BoardEnumerator(seedBoard, maximumGenerations);
        }

        public IEnumerator<bool[,]> GetEnumerator()
        {
            return this.boardEnumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class BoardEnumerator : IEnumerator<bool[,]>
        {
            private readonly int width;
            private readonly int height;
            private readonly int maximumGenerations;
            private int currentGeneration;

            public bool[,] Current { get; private set; }
            object IEnumerator.Current { get => this.Current; }

            public BoardEnumerator(bool[,] seedBoard, int maximumGenerations)
            {
                this.Current = seedBoard;
                this.width = seedBoard.GetLength(0);
                this.height = seedBoard.GetLength(1);

                this.currentGeneration = 0;
                this.maximumGenerations = maximumGenerations;
            }

            public bool MoveNext()
            {
                if(this.currentGeneration > maximumGenerations)
                {
                    return false;
                }

                bool[,] newCanvas = new bool[this.width, this.height];
                //x * y * 3 * 3
                for (int x = 0; x < this.width; x++)
                {
                    for (int y = 0; y < this.height; y++)
                    {
                        int livingNeighbourCount = this.GetLivingNeighbourCount(new Point(x, y));
                        newCanvas[x, y] = (this.Current[x, y] && livingNeighbourCount == 2) || livingNeighbourCount == 3;
                    }
                }

                this.Current = newCanvas;
                return true;
            }

            private int GetLivingNeighbourCount(Point currentCell)
            {
                int result = 0;
                for (int x = currentCell.X - 1; x < currentCell.X + 2; x++)
                {
                    if (x < 0 || x >= this.width)
                    {
                        continue;
                    }
                    for (int y = currentCell.Y - 1; y < currentCell.Y + 2; y++)
                    {
                        if(x == currentCell.X && y == currentCell.Y)
                        {
                            continue;
                        }

                        if (y >= 0 && y < this.height && this.Current[x, y])
                        {
                            result += 1;
                        }
                    }
                }
                return result;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
                this.Current = null;
            }
        }
    }
}
