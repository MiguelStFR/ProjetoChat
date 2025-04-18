namespace ProjetoChat.DTOs.SendPrivateMensage
{
    public class SendPrivateMessageResponse
    {
        public string MessageId { get; set; } = string.Empty;
        public string ConversationId { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
