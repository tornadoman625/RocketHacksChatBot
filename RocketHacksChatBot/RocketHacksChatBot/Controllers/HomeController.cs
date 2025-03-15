using Microsoft.AspNetCore.Mvc;
using OllamaSharp.Models.Chat;
using RocketHacksChatBot.Models;
using RocketHacksChatBot.Services;
using System.Reflection.Metadata.Ecma335;

namespace RocketHacksChatBot.Controllers
{
    [ApiController]
[Route("api/[controller]")]
    public class testController : Controller
    {
        [HttpGet("test")]
        public async Task<AIChatResponse> test()
        {
            CallOllama callOllama = new CallOllama();

            AIChatRequest chatRequest = new AIChatRequest
            {
                history = null,
                message = "When was the University Founded?"
            };
            
            

            var response = await callOllama.AIChat(chatRequest);
            return response;
        }
    }
}

