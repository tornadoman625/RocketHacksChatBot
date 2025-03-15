using Microsoft.AspNetCore.Mvc;
using OllamaSharp.Models.Chat;
using RocketHacksChatBot.Services;
using System.Reflection.Metadata.Ecma335;

namespace RocketHacksChatBot.Controllers
{
    [ApiController]
[Route("api/[controller]")]
    public class testController : Controller
    {
        [HttpGet("test")]
        public string test()
        {
            CallOllama callOllama = new CallOllama();

            ChatRequest chatRequest = new ChatRequest
            {
                mess
            };

            callOllama.AIChat()
        }
    }
}

