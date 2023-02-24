using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Phoneshop.Business.Scrapers
{
    [ExcludeFromCodeCoverage]
    public class TestScraper : IScraper
    {
        private readonly List<Phone> dummyList = new()
        {
            new Phone(),
            new Phone(),
            new Phone()
        };

        private CancellationToken _token;

        public TestScraper(CancellationToken token)
        {
            _token = token;
        }

        public bool CanExecute(string url)
        {
            return true;
        }

        public List<Phone> ExecuteFile(string path, int consoleLine)
        {
            int waitTime = 3000000;

            for (int i = 0; i < waitTime; i++)
            {
                _token.ThrowIfCancellationRequested();

                // "progress reporter" is currently extremely inefficient.
                // it desperately needs some way to slow down the "draw speed"
                // or it will throttle itself with constant locks, as soon
                // as the length of the for loop exceeds a certain amount.
                if (i % 1000 == 0)
                {
                    ProgressReporter.ReportProgress(
                        consoleLine,
                        "Thread " + Environment.CurrentManagedThreadId.ToString(),
                        i,
                        waitTime);
                }
            }

            //while (true)
            //{
            //    await Task.Delay(1000);

            //    Task.Run(() =>
            //    {
            //        _token.ThrowIfCancellationRequested();

            //        ProgressReporter.ReportProgress(
            //        consoleLine,
            //        "Thread " + Environment.CurrentManagedThreadId.ToString(),
            //        0,
            //        100);
            //    });
            //}

            return dummyList;
        }

        public Task<List<Phone>> Execute(string url)
        {
            return Task.FromResult(dummyList);
        }
    }
}
