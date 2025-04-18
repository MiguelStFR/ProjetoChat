using FluentValidation;

namespace ProjetoChat.Utils.Validators.UserController
{
    public class RegisterUserValidator : AbstractValidator<DTOs.User.RegisterUserResquest>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
                .Length(3, 20).WithMessage("O nome de usuário deve ter entre 3 e 20 caracteres.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email deve ser válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres.");
        }
    }
}
