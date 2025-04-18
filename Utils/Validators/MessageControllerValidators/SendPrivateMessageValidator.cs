using FluentValidation;

namespace ProjetoChat.Utils.Validators.MessageControllerValidators
{
    public class SendPrivateMessageValidator : AbstractValidator<DTOs.SendPrivateMensage.SendPrivateMessageRequest>
    {
        public SendPrivateMessageValidator()
        {
            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("O ID do remetente é obrigatório.");
            RuleFor(x => x.ReceiverId)
                .NotEmpty().WithMessage("O ID do destinatário é obrigatório.");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("O conteúdo da mensagem é obrigatório.")
                .Length(1, 500).WithMessage("O conteúdo da mensagem deve ter entre 1 e 500 caracteres.");
        }
    }
}
