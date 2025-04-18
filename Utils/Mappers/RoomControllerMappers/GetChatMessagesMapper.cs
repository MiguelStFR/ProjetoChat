namespace ProjetoChat.Utils.Mappers.RoomControllerMappers
{
    public class GetChatMessagesMapper
    {
        public static DTOs.GetMessages.GetMessagesResponse MapToGetMessagesResponse(List<Models.Message> messages, string roomId)
        {
            return new DTOs.GetMessages.GetMessagesResponse
            {
                RoomId = roomId,
                Messages = messages
            };
        }
    }
}