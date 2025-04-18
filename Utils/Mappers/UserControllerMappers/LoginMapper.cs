namespace ProjetoChat.Utils.Mappers.UserController
{
    public class LoginMapper
    {
        public static DTOs.Login.LoginResponse MapToLoginResponse(Models.User user)
        {
            return new DTOs.Login.LoginResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
