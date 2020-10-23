using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Assessment.Conway
{
    public sealed class GameOfLife : IEnumerable<Board>, IDisposable
    {
        private readonly BoardEnumerator boardEnumerator;

        public GameOfLife(Board seedBoard, int maximumGenerations)
        {
            this.boardEnumerator = new BoardEnumerator(seedBoard, maximumGenerations);
        }

        public IEnumerator<Board> GetEnumerator()
        {
            return this.boardEnumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Dispose()
        {
            this.boardEnumerator.Dispose();
        }

        private sealed class BoardEnumerator : IEnumerator<Board>
        {
            private readonly int maximumGenerations;
            private int currentGeneration;

            public Board Current { get; private set; }

            object IEnumerator.Current { get => this.Current; }

            public BoardEnumerator(Board seedBoard, int maximumGenerations)
            {
                this.Current = seedBoard;

                this.currentGeneration = 0;
                this.maximumGenerations = maximumGenerations;
            }

            public bool MoveNext()
            {
                if (this.currentGeneration >= maximumGenerations)
                {
                    return false;
                }

                if (this.currentGeneration > 0)
                {
                    Board newCanvas = new Board(this.Current.Size);
                    this.Current.Traverse((p, isAlive) =>
                    {
                        int livingNeighbourCount = this.GetLivingNeighbourCount(p);
                        newCanvas[p] = (isAlive && livingNeighbourCount == 2) || livingNeighbourCount == 3;
                    });

                    this.Current = newCanvas;
                }
                this.currentGeneration += 1;

                return true;
            }

            private int GetLivingNeighbourCount(Point currentCell)
            {
                int result = 0;
                for (int x = currentCell.X - 1; x < currentCell.X + 2; x++)
                {
                    if (x < 0 || x >= this.Current.Size.Width)
                    {
                        continue;
                    }
                    for (int y = currentCell.Y - 1; y < currentCell.Y + 2; y++)
                    {
                        if (x == currentCell.X && y == currentCell.Y)
                        {
                            continue;
                        }

                        if (y >= 0 && y < this.Current.Size.Height && this.Current[x, y])
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
                //NOTHING
            }
        }
    }
}
