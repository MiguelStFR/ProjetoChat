namespace ProjetoChat.DTOs.SendPrivateMensage
{
    public class SendPrivateMessageRequest
    {
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
