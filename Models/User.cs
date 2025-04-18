using Google.Cloud.Firestore;

namespace ProjetoChat.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [FirestoreProperty]
        public string Username { get; set; } = string.Empty;
        
        [FirestoreProperty]
        public string Email { get; set; } = string.Empty;

        [FirestoreProperty]
        public string Password { get; set; } = string.Empty;

        [FirestoreProperty]
        public List<string> Rooms { get; set; } = new();
        
        [FirestoreProperty]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}