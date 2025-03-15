using Microsoft.AspNetCore.Mvc;
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
            return "test";
        }
    }
}

