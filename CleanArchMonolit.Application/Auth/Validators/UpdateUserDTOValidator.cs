using CleanArchMonolit.Application.Auth.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMonolit.Application.Auth.Validators
{
    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator() 
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("O nome de usuário é obrigatório.");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("A nova senha é obrigatória").When(x => !string.IsNullOrWhiteSpace(x.OldPassword));
            RuleFor(x => x.ProfileId).GreaterThan(0).WithMessage("Perfil inválido.");
            RuleFor(x => x.Id).LessThanOrEqualTo(0).WithMessage("Não foi possível encontrar o usuário informado, por favor entre em contato com o suporte.");
            RuleFor(x => x.OldPassword).Equal(x => x.NewPassword).WithMessage("A nova senha precisa ser diferente da senha anterior");
        }
    }
}
