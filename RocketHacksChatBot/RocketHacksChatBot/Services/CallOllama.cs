

using OllamaSharp;
using RocketHacksChatBot.Models;

namespace RocketHacksChatBot.Services
{
    public class CallOllama
    {
       public async void AIChat (AIChatRequest request)
        {
            var uri = new Uri("http://localhost:11434");
            var ollama = new OllamaApiClient(uri);
            var chat = new Chat(ollama);

            while (true)
            {
                var message = Console.ReadLine();
                await foreach (var answerToken in chat.SendAsync(request.message))
                    Console.Write(answerToken);
            }

        }
    }
}
