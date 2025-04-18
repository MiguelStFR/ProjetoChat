namespace ProjetoChat.DTOs.EnterRoom
{
    public class EnterRoomRequest
    {
        public string UserId { get; set; } = Guid.Empty.ToString();

        public string Username { get; set; } = string.Empty;
    }
}