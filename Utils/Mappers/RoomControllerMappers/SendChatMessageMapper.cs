namespace ProjetoChat.Utils.Mappers.RoomControllerMappers
{
    public class SendChatMessageMapper
    {
        public static Models.Message MapToMessage(DTOs.SendChatMessage.SendChatMessageRequest request)
        {
            return new Models.Message
            {
                SenderId = request.SenderId,
                Content = request.Content
            };
        }
        public static DTOs.SendChatMessage.SendChatMessageResponse MapToSendMessageResponse(Models.Message message)
        {
            return new DTOs.SendChatMessage.SendChatMessageResponse
            {
                MessageId = message.Id,
                SenderId = message.SenderId,
                SentAt = message.SentAt
            };
        }
    }
}
