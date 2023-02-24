using Phoneshop.Business.Extensions;
using Phoneshop.Domain.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Phoneshop.Scraper
{
    public class ApiClient
    {
        private readonly string apiUrl = "https://localhost:7255/api/Phones";

        public async Task ApiPost(List<Phone> list)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    Console.WriteLine("Starting ApiClient.");
                    client.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    foreach (var phone in list)
                    {
                        Console.WriteLine("Phone: " + phone.FullName());

                        var response = await client.PostAsJsonAsync(apiUrl, phone);

                        Console.WriteLine("Status Code: " + response.StatusCode);

                        response.EnsureSuccessStatusCode();
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex);
            }
        }
    }
}
