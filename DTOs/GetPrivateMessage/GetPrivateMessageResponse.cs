using ProjetoChat.Models;

namespace ProjetoChat.DTOs.GetPrivateMessage
{
    public class GetPrivateMessageResponse
    {
        public string ConversationId { get; set; } = string.Empty;
        public List<Models.PrivateMessage> Messages { get; set; } = new List<Models.PrivateMessage>();
    }
}
