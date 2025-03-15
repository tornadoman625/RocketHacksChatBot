

using OllamaSharp;
using RocketHacksChatBot.Models;

namespace RocketHacksChatBot.Services
{
    public class CallOllama
    {
       public async Task<AIChatResponse> AIChat (AIChatRequest request)
        {
            var uri = new Uri("http://172.26.152.8:11434");
            var ollama = new OllamaApiClient(uri);
            var chat = new Chat(ollama);
            ollama.SelectedModel = "gemma3:1b";
            string message = "";
            await foreach (var answerToken in chat.SendAsync(request.message))
            {
                message += answerToken;
            }
            ChatItem fromUser = new ChatItem
            {
                party = "User",
                message = request.message,

            };

            ChatItem fromAI = new ChatItem
            {
                party = "AI",
                message = message,

            };

            //check if chat history is null, and define it if it is.
            if (request.history is null)
            {
                request.history = new List<ChatItem>();
            }
            request.history.Add(fromUser);
            request.history.Add(fromAI);

            AIChatResponse toReturn = new AIChatResponse
            {
                history = request.history,
                Response = message,
            };

            return toReturn;
            
            
      
                    


        }

        public string composePrompt(string message)
        {
            return "";
        }

    }
}
