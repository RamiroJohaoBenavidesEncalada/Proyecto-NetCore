using FluentValidation;

namespace Curso.ComercioElectronico.Application;

public class ClienteCrearActualizarDtoValidator : AbstractValidator<ClienteCrearActualizarDto>
{
    public ClienteCrearActualizarDtoValidator()
    {

        RuleFor(x => x.Nombres)
            .MinimumLength(3)
            .MaximumLength(256)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");

        RuleFor(x => x.Cedula)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .Length(10).WithMessage("La {PropertyName} es incorrecto");

        RuleFor(x =>x.Direccion)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .Matches(@"^[A-Za-z0-9\s]+$").WithMessage("Solo se acepta letras y números");
        
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");

        RuleFor(x => x.Telefono)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");
        
        RuleFor(x => x.PorcentajeDescuento)
            .GreaterThanOrEqualTo(0)
                .NotNull()
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .WithMessage("El valor ingresado no es correcto. Debe ser un valor mayor o igual a {ComparisonValue}. El valor actual es {PropertyValue}");

    }
}