namespace ProjetoChat.Utils.Mappers.RoomController
{
    public class CreateRoomMapper
    {
        public static Models.Room MapToRoom(DTOs.Room.CreateRoomRequest request)
        {
            return new Models.Room
            {
                Name = request.RoomName,
                Members = request.Members,
                CreatedAt = DateTime.UtcNow
            };
        }
        public static DTOs.Room.CreateRoomResponse MapToCreateRoomResponse(Models.Room room)
        {
            return new DTOs.Room.CreateRoomResponse
            {
                Id = room.Id,
                Name = room.Name,
                Members = room.Members,
                CreatedAt = room.CreatedAt
            };
        }
    }
}