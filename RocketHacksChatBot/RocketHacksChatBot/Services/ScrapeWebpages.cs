using System;
using System.Net.Http;
using System.Threading.Tasks;
namespace RocketHacksChatBot.Services
{
    public class ScrapeWebpages
    {
        public async Task<string> GetHtmlContentAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode(); // Throws exception for bad status codes

                    return await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error retrieving content from {url}: {e.Message}");
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"An unexpected error occured: {e.Message}");
                    return null;
                }
            }
        }
    }
}
