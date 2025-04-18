using Google.Cloud.Firestore;

namespace ProjetoChat.DTOs.SendChatMessage
{
    public class SendChatMessageRequest
    {
        public string SenderId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
