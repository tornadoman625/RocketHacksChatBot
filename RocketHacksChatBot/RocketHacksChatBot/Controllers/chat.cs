using Microsoft.AspNetCore.Mvc;
using OllamaSharp.Models.Chat;
using RocketHacksChatBot.Models;
using RocketHacksChatBot.Services;
using System.Reflection.Metadata.Ecma335;

namespace RocketHacksChatBot.Controllers
{
    [ApiController]
[Route("api/[controller]")]
    public class chatController : Controller
    {
        [HttpPost("submitPrompt")]
        public async Task<AIChatResponse> submitPrompt(AIChatRequest request)
        {
            CallOllama callOllama = new CallOllama();
            var response = await callOllama.AIChat(request);
            return response;
        }

    }
    
}

