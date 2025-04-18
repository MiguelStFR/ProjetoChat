namespace ProjetoChat.Utils.Mappers.RoomController
{
    public class GetRoomMapper
    {
        public static DTOs.GetRoom.GetRoomResponse MapToGetRoomResponse(Models.Room room)
        {
            return new DTOs.GetRoom.GetRoomResponse
            {
                Id = room.Id,
                Name = room.Name,
                Members = room.Members,
                CreatedAt = room.CreatedAt
            };
        }
    }
}
