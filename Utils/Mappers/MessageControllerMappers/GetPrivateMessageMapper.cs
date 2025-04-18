namespace ProjetoChat.Utils.Mappers.MessageControllerMappers
{
    public class GetPrivateMessageMapper
    {
        public static DTOs.GetPrivateMessage.GetPrivateMessageResponse MapToGetPrivateMessageResponse(List<Models.PrivateMessage> privateMessages)
        {
            return new DTOs.GetPrivateMessage.GetPrivateMessageResponse
            {
                ConversationId = privateMessages.FirstOrDefault()?.ConversationId ?? string.Empty,
                Messages = privateMessages
            };
        }
    }
}
