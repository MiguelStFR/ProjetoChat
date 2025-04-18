using Google.Cloud.Firestore;

namespace ProjetoChat.Models
{
    [FirestoreData]
    public class PrivateMessage
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [FirestoreProperty]
        public string SenderId { get; set; } = string.Empty;
        [FirestoreProperty]
        public string ReceiverId { get; set; } = string.Empty;
        [FirestoreProperty]
        public string ConversationId { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Content { get; set; } = string.Empty;
        [FirestoreProperty]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;


        #region Auxiliar Methods

        public void SetConversationId()
        {
            var ordered = new[] { SenderId, ReceiverId }.OrderBy(x => x).ToArray();
            ConversationId = $"{ordered[0]}_{ordered[1]}";
        }

        #endregion
    }
}
