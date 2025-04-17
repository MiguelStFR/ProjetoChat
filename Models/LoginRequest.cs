using Google.Cloud.Firestore;

namespace ProjetoChat.Models
{
    [FirestoreData]
    public class LoginRequest
    {
        [FirestoreProperty]
        public string Username { get; set; } = string.Empty;
    }
}
