﻿namespace ProjetoChat.DTOs.LeaveRoom
{
    public class LeaveRoomResponse
    {
        public string Name { get; set; } = string.Empty;

        public List<string> Members { get; set; } = new();
    }
}
