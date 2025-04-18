using Google.Cloud.Firestore;

namespace ProjetoChat.Models
{
    [FirestoreData]
    public class Message
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [FirestoreProperty]
        public string SenderId { get; set; } = string.Empty;
        
        [FirestoreProperty]
        public string Content { get; set; } = string.Empty;
        
        [FirestoreProperty]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}