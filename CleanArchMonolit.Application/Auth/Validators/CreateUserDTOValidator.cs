using CleanArchMonolit.Application.Auth.DTO;
using FluentValidation;

namespace CleanArchMonolit.Application.Auth.Validators
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("O nome de usuário é obrigatório.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
                .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches("[0-9]").WithMessage("A senha deve conter pelo menos um número.")
                .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter pelo menos um símbolo.");
            RuleFor(x => x.ProfileId).GreaterThan(0).WithMessage("Perfil inválido.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("E-mail inválido");
        }
    }
}
