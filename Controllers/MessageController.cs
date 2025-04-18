using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ProjetoChat.DTOs.GetPrivateMessage;
using ProjetoChat.DTOs.SendPrivateMensage;
using ProjetoChat.Models;
using ProjetoChat.Utils.Mappers.MessageControllerMappers;
using ProjetoChat.Utils.Mappers.RoomControllerMappers;

namespace ProjetoChat.Controllers
{
    [ApiController]
    [Route("messages")]
    public class MessageController : ControllerBase
    {
        private readonly FirestoreDb _firestore;
        public MessageController(FirestoreDb firestore) => _firestore = firestore;

        #region GET

        /// <summary>
        /// Obtém todas as mensagens privadas entre dois usuários.
        /// </summary>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        /// <returns></returns>
        [HttpGet("direct/{conversationId}/messages")]
        public async Task<IActionResult> GetPrivateMessages(string conversationId)
        {
            #region Process Response

            var convRef = _firestore.Collection("private_messages").Document(conversationId);
            var snapshot = await convRef.GetSnapshotAsync();

            if (!snapshot.Exists)
                return NotFound("Nenhuma conversa encontrada.");

            var messagesSnapshot = await convRef.Collection("messages")
                .OrderBy("SentAt")
                .GetSnapshotAsync();

            var messages = messagesSnapshot.Documents
                .Select(doc => doc.ConvertTo<Models.PrivateMessage>())
                .ToList();

            #endregion

            #region Process Response

            var response = GetPrivateMessageMapper.MapToGetPrivateMessageResponse(messages);
            return CreatedAtAction(nameof(GetPrivateMessages), new { id = conversationId }, response);

            #endregion
        }

        #endregion

        #region POST

        /// <summary>
        /// Envia uma mensagem privada entre dois usuários.
        /// </summary>
        /// <param name="serderId"></param>
        /// <param name="receiverId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("direct/{receiverId}")]
        public async Task<IActionResult> SendPrivateMessage(string receiverId, [FromBody] SendPrivateMessageRequest request)
        {
            #region Process Request

            var message = SendPrivateMessageMapper.MapToPrivateMessage(request);

            var convRef = _firestore.Collection("private_messages").Document(message.ConversationId);

            await convRef.SetAsync(new { users = new[] { message.SenderId, receiverId }.OrderBy(x => x).ToArray() }, SetOptions.MergeAll);

            var messageRef = convRef.Collection("messages").Document(message.Id);

            await messageRef.SetAsync(request);

            #endregion

            #region Process Response

            var response = SendPrivateMessageMapper.MapToMessage(message);
            return CreatedAtAction(nameof(GetPrivateMessages), new { response.ConversationId }, response);


            #endregion
        }

        #endregion


    }
}
