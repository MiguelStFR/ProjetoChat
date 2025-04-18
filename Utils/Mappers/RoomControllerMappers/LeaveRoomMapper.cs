namespace ProjetoChat.Utils.Mappers.RoomController
{
    public class LeaveRoomMapper
    {
        public static DTOs.LeaveRoom.LeaveRoomResponse MapToLeaveRoomResponse(Models.Room room)
        {
            return new DTOs.LeaveRoom.LeaveRoomResponse
            {
                Name = room.Name,
                Members = room.Members
            };
        }
    }
}
