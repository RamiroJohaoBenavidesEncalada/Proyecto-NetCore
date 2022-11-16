
using FluentValidation;

namespace Curso.ComercioElectronico.Application;

public class MetodoEntregaCompraCrearActualizarDtoValidator : AbstractValidator<MetodoEntregaCompraCrearActualizarDto>
{
    public MetodoEntregaCompraCrearActualizarDtoValidator()
    {

        RuleFor(x => x.MetodoEntrega)
            .MinimumLength(3)
            .MaximumLength(256)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");

        RuleFor(x =>x.Observaciones)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .Matches(@"^[A-Za-z0-9\s]+$").WithMessage("Solo se acepta letras y números");
        
        RuleFor(x => x.CostoEntrega)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El valor ingresado no es correcto. Debe ser un valor mayor o igual a {ComparisonValue}. El valor actual es {PropertyValue}");

        RuleFor(x =>x.DireccionEntrega)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .Matches(@"^[A-Za-z0-9\s]+$").WithMessage("Solo se acepta letras y números");

    }
}
