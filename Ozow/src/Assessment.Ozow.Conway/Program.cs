using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading;

namespace Assessment.Ozow.Conway
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            Size size = new Size(5, 5);
            int generations = 8;

            Board seedBoard = CreateBoard(size);
            using (GameOfLife gameOfLife = new GameOfLife(seedBoard, generations))
            {
                foreach (Board board in gameOfLife)
                {
                    OutputBoard(board);
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Console.Clear();
                }
            }
        }

        public static Board CreateBoard(Size size)
        {
            Board result = new Board(size);
            int total = (size.Width * size.Height);
            int ratio = (total * 30) / 100;
            
            result.Traverse((p, b) => result[p] = RandomNumberGenerator.GetInt32(0, total) < ratio);
            return result;
        }

        private static void OutputBoard(Board board)
        {
            board.Traverse((p, value) =>
            {
                if (value)
                {
                    Console.Write(" 0 ");
                }
                else
                {
                    Console.Write(" - ");
                }
                if (p.Y == board.Size.Height - 1)
                {
                    Console.WriteLine();
                }
            });
        }
    }
}
