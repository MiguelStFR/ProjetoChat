using FluentValidation;

namespace ProjetoChat.Utils.Validators.RoomController
{
    public class EnterRoomValidator : AbstractValidator<DTOs.EnterRoom.EnterRoomRequest>
    {
        public EnterRoomValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("O ID do usuário é obrigatório.")
                .Must(x => Guid.TryParse(x, out _)).WithMessage("O ID do usuário deve ser um GUID válido.");
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
                .Length(3, 20).WithMessage("O nome de usuário deve ter entre 3 e 20 caracteres.");
        }
    }
}
