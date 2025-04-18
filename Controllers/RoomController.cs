using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using ProjetoChat.DTOs.EnterRoom;
using ProjetoChat.DTOs.LeaveRoom;
using ProjetoChat.DTOs.Room;
using ProjetoChat.DTOs.SendChatMessage;
using ProjetoChat.Models;
using ProjetoChat.Utils.Mappers.RoomController;
using ProjetoChat.Utils.Mappers.RoomControllerMappers;

namespace ProjetoChat.Controllers
{
    [ApiController]
    [Route("rooms")]
    public class RoomController : ControllerBase
    {
        private readonly FirestoreDb _firestore;
        public RoomController(FirestoreDb firestore) => _firestore = firestore;

        #region POST

        /// <summary>
        /// Cria uma nova sala de chat.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
        {

            #region Process Request

            var room = CreateRoomMapper.MapToRoom(request);
            await _firestore.Collection("rooms").Document(room.Id).SetAsync(room);

            #endregion

            #region Process Response

            var response = CreateRoomMapper.MapToCreateRoomResponse(room);
            return CreatedAtAction(nameof(CreateRoom), new { roomId = response.Id }, response);

            #endregion

        }

        /// <summary>
        /// Adiciona um usuário à sala.
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{roomId}/enter")]
        public async Task<IActionResult> EnterRoom(string roomId, [FromBody] EnterRoomRequest request)
        {
            #region Process Request

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

            #endregion

            #region Return Response

            return Ok(new { message = "Usuário adicionado à sala com sucesso." });

            #endregion
        }

        /// <summary>
        /// Remove um usuário da sala.
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{roomId}/leave")]
        public async Task<IActionResult> LeaveRoom(string roomId, [FromBody] LeaveRoomRequest request)
        {
            #region Process Request

            var roomRef = _firestore.Collection("rooms").Document(roomId);
            var snapshot = await roomRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return NotFound("Sala não encontrada.");

            var room = snapshot.ConvertTo<Room>();

            if (!room.Members.Contains(request.UserId))
                return BadRequest("Usuário não está na sala.");

            room.Members.Remove(request.UserId);
            await roomRef.UpdateAsync("Members", room.Members);

            #endregion

            #region Return Response

            return Ok(new { message = "Usuário removido da sala com sucesso." });
         
            #endregion
        }

        /// <summary>
        /// Envia uma mensagem para uma sala de chat.
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("{roonId}/messages")]
        public async Task<IActionResult> SendMessage(string roomId, [FromBody] SendChatMessageRequest request)
        {

            #region Process Request

            var message = SendChatMessageMapper.MapToMessage(request);
            var roomRef = _firestore.Collection("rooms").Document(roomId);
            var roomSnapshot = await roomRef.GetSnapshotAsync();

            if (!roomSnapshot.Exists)
                return NotFound("Sala não encontrada.");

            await roomRef.Collection("messages").Document(message.Id).SetAsync(message);

            #endregion

            #region Process Response

            var response = SendChatMessageMapper.MapToSendMessageResponse(message);
            return CreatedAtAction(nameof(SendMessage), new { messageId = response.MessageId }, response);

            #endregion
        }

        #endregion

        #region GET

        /// <summary>
        /// Retorna os detalhes de uma sala.
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoom(string roomId)
        {
            #region Process Request
            
            var doc = await _firestore.Collection("rooms").Document(roomId).GetSnapshotAsync();

            #endregion
            
            #region Return Response

            if (!doc.Exists)
                return NotFound("Sala não encontrada.");

            var response = GetRoomMapper.MapToGetRoomResponse(doc.ConvertTo<Room>());
            return CreatedAtAction(nameof(GetRoom), new { roomId = response.Id }, response);
            
            #endregion
        }

        /// <summary>
        /// Obtém todas as mensagens de uma sala de chat.
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet("{roomId}/messages")]
        public async Task<IActionResult> GetMessages(string roomId)
        {
            #region Process Request

            var roomRef = _firestore.Collection("rooms").Document(roomId);
            var roomSnapshot = await roomRef.GetSnapshotAsync();

            if (!roomSnapshot.Exists)
                return NotFound("Sala não encontrada.");

            var messagesSnapshot = await roomRef.Collection("messages")
                .OrderBy("SentAt")
                .GetSnapshotAsync();

            var messages = messagesSnapshot.Documents
                .Select(doc => doc.ConvertTo<Message>())
                .ToList();

            #endregion

            #region Process Response

            var response = GetChatMessagesMapper.MapToGetMessagesResponse(messages, roomId);
            return CreatedAtAction(nameof(GetMessages), new { id = roomId }, response);

            #endregion

        }

        #endregion

        #region DELETE

        /// <summary>
        /// Remove um usuário da sala.
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{roomId}/users/{userId}")]
        public async Task<IActionResult> RemoveUserFromRoom(string roomId, string userId)
        {
            #region Process Request

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

            #endregion

            #region Process Response

            return Ok(new { message = $"Usuário '{userId}' removido da sala com sucesso." });
            
            #endregion
        }
        
        #endregion
    }
}
