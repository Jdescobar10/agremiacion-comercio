using FluentValidation;
using GesComercio.Application.DTOs;

namespace GesComercio.Application.Validators;

public class ComercianteValidator : AbstractValidator<ComercianteRequestDto>
{
    public ComercianteValidator()
    {
        RuleFor(x => x.RazonSocial)
            .NotEmpty().WithMessage("La razón social es requerida")
            .MinimumLength(3).WithMessage("La razón social debe tener mínimo 3 caracteres")
            .MaximumLength(150).WithMessage("La razón social no puede superar 150 caracteres");

        RuleFor(x => x.MunicipioId)
            .GreaterThan(0).WithMessage("Debe seleccionar un municipio válido");

        RuleFor(x => x.Telefono)
            .MaximumLength(20).WithMessage("El teléfono no puede superar 20 caracteres")
            .Matches(@"^[0-9+\-\s]*$").WithMessage("El teléfono solo puede contener números")
            .When(x => !string.IsNullOrEmpty(x.Telefono));

        RuleFor(x => x.CorreoElectronico)
            .EmailAddress().WithMessage("El correo electrónico no es válido")
            .MaximumLength(100).WithMessage("El correo no puede superar 100 caracteres")
            .When(x => !string.IsNullOrEmpty(x.CorreoElectronico));
    }
}

