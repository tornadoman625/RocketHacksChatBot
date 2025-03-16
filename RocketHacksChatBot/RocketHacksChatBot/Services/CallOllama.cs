

using OllamaSharp;
using RocketHacksChatBot.Models;

namespace RocketHacksChatBot.Services
{
    public class CallOllama
    {
       public async Task<AIChatResponse> AIChat (AIChatRequest request)
        {
            var uri = new Uri("http://172.26.99.96:11434");
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

            var scraper = new ScrapeWebpages();
            String webPages = "";
            String chatHistory = "";
            OllamaRAGSystem rag = new OllamaRAGSystem();

            
            if (history is not null)
            {
                foreach (ChatItem item in history)
                {
                    chatHistory += "{" + item.party + ": " + item.message + "}";
                }
            } else { chatHistory = "No chat history yet."; }
            string prompt = $@"
            You are a helpful assistant specializing in information about the University of Toledo. You will be provided with HTML content from various web pages related to the University of Toledo. Use this information to answer questions accurately and comprehensively.

            **Important Guidelines:**

            1.  **University of Toledo Focus:** All questions you answer MUST be related to the University of Toledo. If a question is outside of this scope, politely state, ""I can only answer questions related to the University of Toledo.""
            2.  **HTML Context:** You will be provided with a large amount of HTML content. Treat this as your primary source of information. Answer in simple text, no syntax.
            3.  **Accuracy:** Strive for accuracy. If the provided HTML doesn't contain the information needed to answer a question, state, ""I cannot find the answer to your question within the provided information.""
            4.  **Chat History:** You will be provided with a chat history. Use this to maintain context and provide coherent responses.
            5.  **Conciseness and Clarity:** While comprehensiveness is important, prioritize clear and concise answers.
            6.  **No External Browsing:** You do not have access to the internet. Only use the provided HTML.
            7.  **Answer in the language that the user asks the question.**

            **context:**

            {await rag.getContext(message)}

            **Chat History:**

            {chatHistory}

            **User Question:**

            {message}
";
            return prompt;


        }

    }
}

