using Google.Cloud.Firestore;

namespace ProjetoChat.Models
{
    [FirestoreData]
    public class Room
    {
        [FirestoreProperty]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [FirestoreProperty]
        public string Name { get; set; } = string.Empty;
        
        [FirestoreProperty]
        public List<string> Members { get; set; } = new();
        
        [FirestoreProperty]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
