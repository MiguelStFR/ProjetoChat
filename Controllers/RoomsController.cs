using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using ProjetoChat.Models;

namespace ProjetoChat.Controllers
{
    [ApiController]
    [Route("rooms")]
    public class RoomsController : ControllerBase
    {
        private readonly FirestoreDb _firestore;
        public RoomsController(FirestoreDb firestore) => _firestore = firestore;

        [HttpPost]
        public async Task<IActionResult> CreateRoom(Room room)
        {
            if (string.IsNullOrWhiteSpace(room.Name))
                return BadRequest("O nome da sala é obrigatório.");

            var roomRef = _firestore.Collection("rooms").Document(room.Id);
            await roomRef.SetAsync(room);

            return CreatedAtAction(nameof(GetRoom), new { roomId = room.Id }, room);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoom(string roomId)
        {
            var doc = await _firestore.Collection("rooms").Document(roomId).GetSnapshotAsync();

            if (!doc.Exists)
                return NotFound("Sala não encontrada.");

            var room = doc.ConvertTo<Room>();
            return Ok(room);
        }

        [HttpPost("{roomId}/enter")]
        public async Task<IActionResult> EnterRoom(string roomId, [FromBody] RoomRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
                return BadRequest("O ID do usuário é obrigatório.");

            var roomRef = _firestore.Collection("rooms").Document(roomId);
            var snapshot = await roomRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return NotFound("Sala não encontrada.");

            var room = snapshot.ConvertTo<Room>();

            if (!room.Members.Contains(request.UserId))
            {
                room.Members.Add(request.UserId);
                await roomRef.UpdateAsync("Members", room.Members);
            }

            return Ok(new { message = "Usuário adicionado à sala com sucesso." });
        }

        [HttpPost("{roomId}/leave")]
        public async Task<IActionResult> LeaveRoom(string roomId, [FromBody] RoomRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId))
                return BadRequest("O ID do usuário é obrigatório.");

            var roomRef = _firestore.Collection("rooms").Document(roomId);
            var snapshot = await roomRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return NotFound("Sala não encontrada.");

            var room = snapshot.ConvertTo<Room>();

            if (!room.Members.Contains(request.UserId))
                return BadRequest("Usuário não está na sala.");

            room.Members.Remove(request.UserId);
            await roomRef.UpdateAsync("Members", room.Members);

            return Ok(new { message = "Usuário removido da sala com sucesso." });
        }

        [HttpDelete("{roomId}/users/{userId}")]
        public async Task<IActionResult> RemoveUserFromRoom(string roomId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest("O ID do usuário é obrigatório.");

            var roomRef = _firestore.Collection("rooms").Document(roomId);
            var snapshot = await roomRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return NotFound("Sala não encontrada.");

            var room = snapshot.ConvertTo<Room>();

            if (!room.Members.Contains(userId))
                return BadRequest("Usuário não está na sala.");

            room.Members.Remove(userId);
            await roomRef.UpdateAsync("Members", room.Members);

            return Ok(new { message = $"Usuário '{userId}' removido da sala com sucesso." });
        }
    }
}
