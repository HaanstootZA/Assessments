using System;
using System.Drawing;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;

namespace Assessment.Ozow.Conway
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            void onCancelKeyPress(object sender, ConsoleCancelEventArgs args)
            {
                args.Cancel = true;
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Token.ThrowIfCancellationRequested();
            };
            Console.CancelKeyPress += onCancelKeyPress;

            try
            {
                int width = GetConsoleInput("What is the width of the board?");
                int height = GetConsoleInput("What is the height of the board?");
                int generations = GetConsoleInput("How many time must the board generate?");

                Size size = new Size(width, height);
                Board seedBoard = CreateBoard(size);
                int i = 0;
                using (GameOfLife gameOfLife = new GameOfLife(seedBoard, generations))
                {
                    foreach (Board board in gameOfLife)
                    {
                        Console.Clear();
                        if (OutputBoard(board))
                        {
                            Console.WriteLine("I guess that's the end of our little society.");
                            break;
                        }
                        i += 1;
                        Thread.Sleep(TimeSpan.FromSeconds(1));
                    }
                }
                Console.WriteLine($"(Generation: {i}) - Output Completed! Press any key to exit.");
                Console.ReadKey();
            }
            catch (OperationCanceledException)
            {
                //DO NOTHING
            }
            Console.CancelKeyPress -= onCancelKeyPress;
        }

        private static int GetConsoleInput(string message)
        {
            string input;
            int result;
            do
            {
                Console.WriteLine(message);
                input = Console.ReadLine();
            }
            while (!int.TryParse(input, out result));
            return result;
        }

        public static Board CreateBoard(Size size)
        {
            if (size.Width == 42 && size.Height == 42)
            {
                return new Board(new bool[,]{
                    {false, false, false, false, true, false, false, false, false, true, true, true, false},
                    {false, false, false, true, false, false, false, false, true, false, false, false, true},
                    {false, false, true, false, false, false, false, false, false, false, false, true, false},
                    {false, true, false, false, true, false, false, false, false, true, false, false, false},
                    {true, true, true, true, true, true, false, false, true, false, false, false, false},
                    {false, false, false, false, true, false, false, false, true, true, true, true, true}
                });
            }

            Board result = new Board(size);
            int total = (size.Width * size.Height);
            int ratio = (total * 40) / 100;

            result.Traverse((p, b) => result[p] = RandomNumberGenerator.GetInt32(0, total) < ratio);
            return result;
        }

        private static bool OutputBoard(Board board)
        {
            bool finished = true;
            board.Traverse((p, value) =>
            {
                if (value)
                {
                    finished = false;
                     Console.Write("0");
                }
                else
                {
                    Console.Write(".");
                }
                if (p.Y == board.Size.Height - 1)
                {
                    Console.WriteLine();
                }
            });

            return finished;
        }
    }
}
