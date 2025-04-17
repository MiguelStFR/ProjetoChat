using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using ProjetoChat.Models;
using LoginRequest = ProjetoChat.Models.LoginRequest;

namespace ProjetoChat.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly FirestoreDb _firestore;
        public UsersController(FirestoreDb firestore) => _firestore = firestore;

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                return BadRequest("O nome de usuário é obrigatório.");

            var userRef = _firestore.Collection("users").Document(user.Id);
            await userRef.SetAsync(user);

            return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username))
                return BadRequest("O nome de usuário é obrigatório.");

            var query = _firestore
            .Collection("users")
                .WhereEqualTo("Username", request.Username);

            var snapshot = await query.GetSnapshotAsync();

            if (snapshot.Count == 0)
                return NotFound("Usuário não encontrado.");

            var user = snapshot.Documents[0].ConvertTo<User>();

            return Ok(user);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var userDoc = await _firestore.Collection("users").Document(userId).GetSnapshotAsync();

            if (!userDoc.Exists)
                return NotFound();

            var user = userDoc.ConvertTo<User>();
            return Ok(user);
        }
    }
}
