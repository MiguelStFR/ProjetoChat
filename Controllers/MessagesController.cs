using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using ProjetoChat.Models;

namespace ProjetoChat.Controllers
{
    [ApiController]
    [Route("messages")]
    public class MessagesController : ControllerBase
    {
        private readonly FirestoreDb _firestore;
        public MessagesController(FirestoreDb firestore) => _firestore = firestore;

        [HttpPost]
        public async Task<IActionResult> SendMessage(string roomId, [FromBody] ChatMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.SenderId) || string.IsNullOrWhiteSpace(message.Content))
                return BadRequest("SenderId e Content são obrigatórios.");

            var roomRef = _firestore.Collection("rooms").Document(roomId);
            var roomSnapshot = await roomRef.GetSnapshotAsync();

            if (!roomSnapshot.Exists)
                return NotFound("Sala não encontrada.");

            var messagesRef = roomRef.Collection("messages").Document(message.Id);
            await messagesRef.SetAsync(message);

            return Ok(new { message = "Mensagem enviada com sucesso." });
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages(string roomId)
        {
            var roomRef = _firestore.Collection("rooms").Document(roomId);
            var roomSnapshot = await roomRef.GetSnapshotAsync();

            if (!roomSnapshot.Exists)
                return NotFound("Sala não encontrada.");

            var messagesSnapshot = await roomRef.Collection("messages")
                .OrderBy("SentAt")
                .GetSnapshotAsync();

            var messages = messagesSnapshot.Documents
                .Select(doc => doc.ConvertTo<ChatMessage>())
                .ToList();

            return Ok(messages);
        }
    }
}
