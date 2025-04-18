namespace ProjetoChat.DTOs.LeaveRoom
{
    public class LeaveRoomRequest
    {
        public string UserId { get; set; } = Guid.Empty.ToString();
    }
}
