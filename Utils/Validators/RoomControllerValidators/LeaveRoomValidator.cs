using FluentValidation;

namespace ProjetoChat.Utils.Validators.RoomController
{
    public class LeaveRoomValidator : AbstractValidator<DTOs.LeaveRoom.LeaveRoomRequest>
    {
        public LeaveRoomValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("O ID do usuário não pode ser vazio");
        }
    }
}
