using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using ProjetoChat.Models;

namespace ProjetoChat.Controllers
{
    public class PrivateMessageController : ControllerBase
    {
        private readonly FirestoreDb _firestore;

        public PrivateMessageController(FirestoreDb firestore) => _firestore = firestore;

        private string GetConversationId(string userA, string userB)
        {
            var ordered = new[] { userA, userB }.OrderBy(x => x).ToArray();
            return $"{ordered[0]}_{ordered[1]}";
        }

        [HttpPost("{userA}/{userB}/messages")]
        public async Task<IActionResult> SendPrivateMessage(string userA, string userB, [FromBody] PrivateMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.SenderId) || string.IsNullOrWhiteSpace(message.Content))
                return BadRequest("SenderId e Content são obrigatórios.");

            var conversationId = GetConversationId(userA, userB);
            var convRef = _firestore.Collection("private_messages").Document(conversationId);

            await convRef.SetAsync(new { users = new[] { userA, userB } }, SetOptions.MergeAll);

            var messageRef = convRef.Collection("messages").Document(message.Id);
            await messageRef.SetAsync(message);

            return Ok(new { message = "Mensagem privada enviada com sucesso." });
        }

        [HttpGet("{userA}/{userB}/messages")]
        public async Task<IActionResult> GetPrivateMessages(string userA, string userB)
        {
            var conversationId = GetConversationId(userA, userB);
            var convRef = _firestore.Collection("private_messages").Document(conversationId);
            var snapshot = await convRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return NotFound("Nenhuma conversa encontrada.");

            var messagesSnapshot = await convRef.Collection("messages")
                .OrderBy("SentAt")
                .GetSnapshotAsync();

            var messages = messagesSnapshot.Documents
                .Select(doc => doc.ConvertTo<PrivateMessage>())
                .ToList();

            return Ok(messages);
        }
    }
}
