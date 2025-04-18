using FluentValidation;

namespace ProjetoChat.Utils.Validators.RoomController
{
    public class CreateRoomValidator : AbstractValidator<DTOs.Room.CreateRoomRequest>
    {
        public CreateRoomValidator()
        {
            RuleFor(x => x.RoomName)
                .NotEmpty().WithMessage("O nome da sala é obrigatório.")
                .Length(3, 20).WithMessage("O nome da sala deve ter entre 3 e 20 caracteres.");
            RuleFor(x => x.Members)
                .NotEmpty().WithMessage("Os membros são obrigatórios.")
                .Must(members => members.Count > 1).WithMessage("Devem haver pelo menos dois membros na sala.");
        }
    }
}
