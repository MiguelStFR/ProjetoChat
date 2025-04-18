using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using ProjetoChat.DTOs.Login;
using ProjetoChat.DTOs.User;
using ProjetoChat.Models;
using ProjetoChat.Utils.Mappers.UserController;

namespace ProjetoChat.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly FirestoreDb _firestore;
        public UserController(FirestoreDb firestore) => _firestore = firestore;

        #region Post

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserResquest request)
        {
            #region Process Request
            
            var user = RegisterUserMapper.MapToUser(request);
            await _firestore.Collection("users").Document(user.Id).SetAsync(user);

            #endregion

            #region Return Response

            var response = RegisterUserMapper.MapToResponse(user);
            return CreatedAtAction(nameof(Register), new { id = user.Id }, response);

            #endregion
        }

        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            #region Process Request

            var query = _firestore.Collection("users").WhereEqualTo("Username", request.Username).WhereEqualTo("Password", request.Password);
            var snapshot = await query.GetSnapshotAsync();

            #endregion

            #region Return Response

            if (snapshot.Count == 0)
                return NotFound("Usuário não encontrado.");

            var response = LoginMapper.MapToLoginResponse(snapshot.Documents[0].ConvertTo<User>());
            return CreatedAtAction(nameof(Login), new { id = response.Id }, response);

            #endregion
        }

        #endregion Post

        #region Get

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            #region Process Request

            var userDoc = await _firestore.Collection("users").Document(userId).GetSnapshotAsync();

            #endregion

            #region Return Response

            if (!userDoc.Exists)
                return NotFound();

            var response = GetUserMapper.MapToGetUserResponse(userDoc.ConvertTo<User>());
            return CreatedAtAction(nameof(GetUser), new { id = response.Id }, response);

            #endregion Response
        }

        #endregion Get
    }
}
