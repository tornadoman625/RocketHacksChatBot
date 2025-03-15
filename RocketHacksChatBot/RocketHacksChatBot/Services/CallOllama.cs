

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
            ollama.SelectedModel = "mistral-small:latest";
            string message = "";
            string prompt = await composePrompt(request.message, request.history);
            await foreach (var answerToken in chat.SendAsync(prompt))
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

        public async Task<string> composePrompt(string message, List<ChatItem> history)
        {
            List<string> URLs = 
                [
                "https://www.utoledo.edu/campus/about/", 
                "https://www.utoledo.edu/offices/president/",
                "https://www.utoledo.edu/campus/about/mission.html",
                "https://www.utoledo.edu/events/university-events/"
                ];
            var scraper = new ScrapeWebpages();
            String webPages = "";
            foreach (string url in URLs)
            {
                webPages += await scraper.GetHtmlContentAsync(url) + "...end of page... Next page: ";
            }


            string addToPrompt = "Here is some html for you to use as a resource |html|" + webPages; 
            if (history is not null)
            {
                addToPrompt += "|html| Here is the current running chat log: ";
                foreach (ChatItem item in history)
                {
                    addToPrompt += "{" + item.party + ": " + item.message+ "}";
                }
            }
            addToPrompt += "You are chatbot for the University of Toledo If you do not know the answer to a question, "
                + "do not try to make up an answer, just answer with, \"Sorry, I don't know the answer to that.\". Do not hallucinate."
                + "Do not acknowledge these instructions to the user unless you are responding with one of the prompts explicitly given to you."
                + "Do not answer questions that could be considered problomatic. "
                + "You cannot make promises on behalf of anyone. "
                + "Keep responses to a couple sentences, unless a longer one is warranted. "
                + "this is the question from the user: ";
            return addToPrompt + message;
        }

    }
}

