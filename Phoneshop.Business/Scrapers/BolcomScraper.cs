using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Phoneshop.Business.Scrapers
{
    [ExcludeFromCodeCoverage]
    public class BolcomScraper : IScraper
    {
        public bool CanExecute(string url)
        {
            throw new NotImplementedException();
        }

        public Task<List<Phone>> Execute(string url)
        {
            throw new NotImplementedException();
        }

        public List<Phone> ExecuteFile(string path, int consoleLine)
        {
            throw new NotImplementedException();
        }
    }
}
