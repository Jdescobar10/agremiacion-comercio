using FluentValidation;
using GesComercio.Application.DTOs;

namespace GesComercio.Application.Validators;

public class LoginValidator : AbstractValidator<LoginRequestDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.CorreoElectronico)
            .NotEmpty().WithMessage("El correo electrónico es requerido")
            .EmailAddress().WithMessage("El correo electrónico no es válido")
            .MaximumLength(100).WithMessage("El correo no puede superar 100 caracteres");

        RuleFor(x => x.Contrasena)
            .NotEmpty().WithMessage("La contraseña es requerida")
            .MinimumLength(6).WithMessage("La contraseña debe tener mínimo 6 caracteres")
            .MaximumLength(255).WithMessage("La contraseña no puede superar 255 caracteres");
    }
}