using System;
using System.Diagnostics.CodeAnalysis;

namespace Phoneshop.Business.Scrapers
{
    [ExcludeFromCodeCoverage]
    public static class ProgressReporter
    {
        static private readonly object _sync = new();

        public static void ReportProgress(int cursorY, string item, int progress, int total)
        {
            // attempt to slow down the "draw speed"
            if (progress > 0 && progress / total % 1 == 0)
            {
                int perc = (int)100.0 * progress / total;

                lock (_sync)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = cursorY;
                    Console.Write(item + " [" + new string('=', perc / 2) + "] " + perc + "%");
                }
            }
        }
    }
}
