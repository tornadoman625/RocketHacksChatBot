using RocketHacksChatBot.Client.Models;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;

namespace RocketHacksChatBot.Client.Services
{
    public class useAPI
    {
        private HttpClient client = new HttpClient();
        public useAPI()
        {
            client.BaseAddress = new Uri("https://localhost:7250");
        }
        public async Task<AIChatResponse> makeAPICall(AIChatRequest request)
        {
            
            if (request == null)
            {
                return null;
            }
            var response = await client.PostAsJsonAsync<AIChatRequest>("/api/chat/submitPrompt", request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AIChatResponse>();
            }
            return null;
        }
         
    }
}
