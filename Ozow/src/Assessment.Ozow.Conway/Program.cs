using System;
using System.Drawing;
using System.Threading;

namespace Assessment.Ozow.Conway
{
    public class Program
    {
        static void Main(string[] args)
        {
            Size size = new Size(5, 5);
            GameOfLife gameOfLife = new GameOfLife(new bool[size.Width, size.Height], 8);
            foreach (bool[,] board in gameOfLife)
            {
                for (int y = 0; y < size.Height; y++)
                {
                    for (int x = 0; x < size.Width; x++)
                    {

                        if (board[x, y])
                        {
                            Console.Write(" 0 ");
                        }
                        else
                        {
                            Console.Write(" X ");
                        }
                    }
                    Console.WriteLine();
                }
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.Clear();
            }
        }
    }
}
