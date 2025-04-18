namespace ProjetoChat.DTOs.SendChatMessage
{
    public class SendChatMessageResponse
    {
        public string MessageId { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
