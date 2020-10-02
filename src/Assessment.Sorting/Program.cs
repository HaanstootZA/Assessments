using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Assessment.Sorting
{
    class Program
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
                IStringSorter sorter = new SimpleSorter();
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    SortString(sorter);
                }
            }
            catch (OperationCanceledException)
            {
                //DO NOTHING
            }
            Console.CancelKeyPress -= onCancelKeyPress;
        }

        private static void SortString(IStringSorter sorter)
        {
            Console.WriteLine("Input the string that requires sorting, then press enter.");
            string unsortedString = Console.ReadLine();
            string sortedString = sorter.SortString(unsortedString);
            Console.WriteLine("The result of the sorting is:");
            Console.WriteLine(sortedString);
        }
    }
}
