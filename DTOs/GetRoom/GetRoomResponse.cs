namespace ProjetoChat.DTOs.GetRoom
{
    public class GetRoomResponse
    {
        public string Id { get; set; } = Guid.Empty.ToString();

        public string Name { get; set; } = string.Empty;

        public List<string> Members { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
