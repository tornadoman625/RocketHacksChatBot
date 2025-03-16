using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace RocketHacksChatBot.Services
{
    public class ScrapeWebpages
    {
        private static readonly HttpClient client = new HttpClient();
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
        public  async Task<List<string>> SearchBing(string query, string siteUrl, int numResults = 10)
        {
            try
            {
                string encodedQuery = Uri.EscapeDataString($"site:{siteUrl} {query}"); //Modified query to include site: filter
                string url = $"https://www.bing.com/search?q={encodedQuery}&count={numResults}";

                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");

                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string html = await response.Content.ReadAsStringAsync();

                return ExtractResults(html);

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private static List<string> ExtractResults(string html)
        {
            var results = new List<string>();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            // Bing search result links are typically within <a> tags inside <li> elements with class "b_algo".
            var linkNodes = htmlDoc.DocumentNode.SelectNodes("//li[contains(@class, 'b_algo')]//a[h2] | //li[contains(@class, 'b_algo')]//a[div/h2]");

            if (linkNodes != null)
            {
                foreach (var linkNode in linkNodes)
                {
                    string href = linkNode.GetAttributeValue("href", "");
                    if (!string.IsNullOrEmpty(href))
                    {
                        results.Add(href);
                    }
                }
            }

            return results;
        }
    }
}
