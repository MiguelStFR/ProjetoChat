namespace ProjetoChat.Utils.Mappers.UserController
{
    public class GetUserMapper
    {
        public static DTOs.GetUser.GetUserResponse MapToGetUserResponse(Models.User user)
        {
            return new DTOs.GetUser.GetUserResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
