using ProjetoChat.DTOs.SendPrivateMensage;
using ProjetoChat.Models;

namespace ProjetoChat.Utils.Mappers.MessageControllerMappers
{
    public class SendPrivateMessageMapper
    {
        public static Models.PrivateMessage MapToPrivateMessage(SendPrivateMessageRequest request)
        {
            PrivateMessage message = new Models.PrivateMessage
            {
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId,
                Content = request.Content,
                SentAt = DateTime.UtcNow
            };

            message.SetConversationId();
            return message;
        }
        public static SendPrivateMessageResponse MapToMessage(Models.PrivateMessage privateMessage)
        {
            return new SendPrivateMessageResponse
            {
                MessageId = privateMessage.Id,
                ConversationId = privateMessage.SenderId,
                SentAt = privateMessage.SentAt
            };
        }
    }
}
