using Phoneshop.Business.Extensions;
using Phoneshop.Business.Scrapers;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System.Collections.Concurrent;

namespace Phoneshop.Scraper;

public class Program
{
    private static readonly IScraper _belsimpelScraper = new BelsimpelScraper();
    private static readonly IScraper _vodafoneScraper = new VodafoneScraper();
    private static readonly List<IScraper> _scraperList = new()
    {
        _belsimpelScraper,
        _vodafoneScraper
    };
    private static List<string> _scraperUrls;

    // For parallel testing
    private static CancellationTokenSource _ctSource = new();
    private static CancellationToken _token = _ctSource.Token;
    private static ConcurrentBag<List<Phone>> _phoneBag = new();
    private static int _degreeParallel = 5;
    private static List<(IScraper, string)> _scraperTuples;
    private static TestScraper[] _testArray;

    private static void Main(string[] args)
    {
        string path = args[0];
        _scraperUrls = new()
        {
            path + "\\belsimpel.html",
            path + "\\vodafone.html"
        };

        // for Parallel testing
        _scraperTuples = new();
        for (int i = 0; i < 10; i++)
        {
            _scraperTuples.Add((_belsimpelScraper, path + "\\belsimpel.html"));
            _scraperTuples.Add((_vodafoneScraper, path + "\\vodafone.html"));
        }

        MainMenu();
    }

    private static void MainMenu()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Please enter the number for one of the following options:");
            Console.WriteLine("1. Scrape all");
            Console.WriteLine("2. Scrape specific url");
            Console.WriteLine("3. Scrape all in parallel (test)");
            Console.WriteLine("4. Scrape all in parallel with progress bars (test)");
            Console.WriteLine("5. Exit");

            var pressed = Console.ReadLine();
            Console.Clear();

            switch (pressed)
            {
                case "1":
                    ScrapeAllMenu();
                    break;
                case "2":
                    ScrapeUrlMenu();
                    break;
                case "3":
                    StartAllParallelTest();
                    break;
                case "4":
                    StartAllSemaphoreTest().Wait();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please select a valid option.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void ScrapeAllMenu()
    {
        foreach (var url in _scraperUrls)
        {
            var list = ScrapePhones(url);
            if (list == null) EndScraping();
            WritePhonesToConsole(list).Wait();
        }

        EndScraping();
    }

    private static void ScrapeUrlMenu()
    {
        int index = 1;
        foreach (var scraper in _scraperList)
        {
            Console.WriteLine($"{index}. {scraper}");
            index++;
        }
        var input = Console.ReadLine();
        if (int.TryParse(input, out int result))
        {
            List<Phone> scrapedPhones = ScrapePhones(_scraperUrls[result - 1]);
            if (scrapedPhones == null) EndScraping();
            WritePhonesToConsole(scrapedPhones).Wait();
        }

        EndScraping();
    }

    private static async Task StartAllSemaphoreTest()
    {
        int scrCount = 20;
        _testArray = new TestScraper[scrCount];

        for (int i = 0; i < scrCount; i++)
        {
            _testArray[i] = new TestScraper(_token);
        }

        var tasks = new List<Task>();

        int cursorY = 0;

        AnnounceAndEnableCancelRequests(scrCount);

        using (var semaphore = new SemaphoreSlim(_degreeParallel))
        {
            try
            {
                for (int i = 0; i < scrCount; i++)
                {
                    int j = i; // avoid closure bug

                    await semaphore.WaitAsync(_token);

                    tasks.Add(Task.Run(() =>
                    {
                        try
                        {
                            _token.ThrowIfCancellationRequested();
                            _phoneBag.Add(_testArray[j].ExecuteFile("test", cursorY));
                            Interlocked.Increment(ref cursorY);

                            if (cursorY > 4) Interlocked.Exchange(ref cursorY, 0);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            throw;
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    }));
                }

                await Task.WhenAll(tasks);
                WritePhoneBagCountToConsole();

            }
            catch (OperationCanceledException cancelEx)
            {
                Console.WriteLine(cancelEx.Message);
            }
            finally
            {
                _ctSource.Dispose();
                semaphore.Dispose();
            }
        }

        EndScraping();
    }

    private static void StartAllParallelTest()
    {
        ParallelOptions parOptions = new();
        parOptions.CancellationToken = _token;
        parOptions.MaxDegreeOfParallelism = _degreeParallel;

        AnnounceAndEnableCancelRequests(_scraperTuples.Count);

        try
        {
            Parallel.ForEach(_scraperTuples, parOptions, (scraperTuple, loopState) =>
            {
                // When cancellation is requested, only planned tasks will be cancelled.
                // Cancellation requests will do nothing when the test list is too small.
                IScraper scraper = scraperTuple.Item1;
                string url = scraperTuple.Item2;
                if (scraper.CanExecute(url))
                {
                    _phoneBag.Add(scraper.ExecuteFile(url));
                }
                else loopState.Break();
            });
            Console.Clear();
            Console.WriteLine("Operation ran to completion.");
        }
        catch (OperationCanceledException ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        finally
        {
            _ctSource.Dispose();
        }

        WritePhoneBagCountToConsole();

        EndScraping();
    }

    /// <summary>
    /// States the scraper count, then waits for a key press before starting a task to
    /// check for cancellation which can then be communicated to the token source.
    /// </summary>
    /// <param name="scraperCount"></param>
    private static void AnnounceAndEnableCancelRequests(int scraperCount)
    {
        Console.WriteLine($"Testing with {scraperCount} scrapers to run in parallel, " +
            $"{_degreeParallel} at a time.");
        Console.WriteLine("Press any key to start. To cancel the operation in progress, press 'c'.");
        Console.ReadKey();

        Console.Clear();
        Task.Factory.StartNew(() =>
        {
            if (Console.ReadKey().KeyChar == 'c')
            {
                _ctSource.Cancel();
                Console.WriteLine("\nAttempting to cancel planned tasks...");
            }
        });
    }

    private static void EndScraping()
    {
        Console.WriteLine("\nPress any key to close this program.");
        Console.ReadKey();
        Environment.Exit(0);
    }

    private static List<Phone> ScrapePhones(string url)
    {
        foreach (var scraper in _scraperList)
        {
            if (scraper.CanExecute(url))
            {
                try
                {
                    return scraper.ExecuteFile(url);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error when calling ExecuteFile.");
                    throw;
                }
            }
        }
        Console.WriteLine("CanExecute did not return true for any scraper.");
        return null;
    }

    private static async Task WritePhonesToConsole(List<Phone> scrapedPhones)
    {
        if (scrapedPhones != null)
        {
            Console.Clear();
            foreach (Phone phone in scrapedPhones)
            {
                Console.WriteLine("Phone name:");
                Console.WriteLine(phone.FullName());
                Console.WriteLine("Price:");
                Console.WriteLine(phone.Price);
                Console.WriteLine();
            }

            ApiClient apiClient = new();

            List<Phone> testList = new();
            testList.Add(new Phone
            {
                Type = "scrape test 1",
                Brand = new Brand() { Name = "scrape test" },
                Description = "test",
                Price = 1,
                Stock = 1
            });
            testList.Add(new Phone
            {
                Type = "scrape test 2",
                Brand = new Brand() { Name = "scrape test" },
                Description = "test",
                Price = 1,
                Stock = 1
            });

            await apiClient.ApiPost(testList);
        }
    }

    private static void WritePhoneBagCountToConsole()
    {
        List<Phone> phoneList = new();

        foreach (var list in _phoneBag)
        {
            phoneList.AddRange(list);
        }
        Console.Clear();
        Console.WriteLine($"Phones found via scrapers: {phoneList.Count}");
    }
}
