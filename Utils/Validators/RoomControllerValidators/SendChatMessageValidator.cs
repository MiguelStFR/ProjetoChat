using FluentValidation;

namespace ProjetoChat.Utils.Validators.RoomControllerValidators
{
    public class SendChatMessageValidator : AbstractValidator<DTOs.SendChatMessage.SendChatMessageRequest>
    {
        public SendChatMessageValidator()
        {
            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("O ID do remetente é obrigatório.");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("O conteúdo da mensagem é obrigatório.")
                .Length(1, 500).WithMessage("O conteúdo da mensagem deve ter entre 1 e 500 caracteres.");
        }
    }
}
