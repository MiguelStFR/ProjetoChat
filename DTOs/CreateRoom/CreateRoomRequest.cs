namespace ProjetoChat.DTOs.Room
{
    public class CreateRoomRequest
    {
        public string RoomName { get; set; } = string.Empty;

        public List<string> Members { get; set; } = new();
    }
}
