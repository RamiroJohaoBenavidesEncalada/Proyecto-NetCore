using FluentValidation;

namespace Curso.ComercioElectronico.Application;

public class TipoProductoCrearActualizarDtoValidator : AbstractValidator<TipoProductoCrearActualizarDto>
{
    public TipoProductoCrearActualizarDtoValidator()
    {

        RuleFor(x => x.Nombre)
            .MinimumLength(3)
            .MaximumLength(256)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");

    }
}