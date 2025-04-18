using ProjetoChat.Models;

namespace ProjetoChat.DTOs.GetMessages
{
    public class GetMessagesResponse
    {
        public string RoomId { get; set; } = string.Empty;
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}