using Phoneshop.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoneshop.Domain.Interfaces
{
    public interface IScraper
    {
        public bool CanExecute(string url);

        public Task<List<Phone>> Execute(string url);

        // method signature was modified to allow for a y coordinate
        // that is intended to support multiple console progress bars
        // see: ProgressReporter
        public List<Phone> ExecuteFile(string path, int consoleLine = 0);
    }
}
