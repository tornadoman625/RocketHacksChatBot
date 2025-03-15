namespace RocketHacksChatBot.Client.Models
{
    public class AIChatRequest
    {
        public List<ChatItem>? history { get; set; }
        public string message { get; set; }
    }
}
