namespace ProjetoChat.Utils.Mappers.UserController
{
    public class RegisterUserMapper
    {
        public static Models.User MapToUser(DTOs.User.RegisterUserResquest request)
        {
            return new Models.User
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };
        }
        public static DTOs.User.RegisterUserResponse MapToResponse(Models.User user)
        {
            return new DTOs.User.RegisterUserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }
    }
}