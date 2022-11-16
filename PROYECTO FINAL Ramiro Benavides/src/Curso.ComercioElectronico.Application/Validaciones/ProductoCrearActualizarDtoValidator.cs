using FluentValidation;

namespace Curso.ComercioElectronico.Application;

public class ProductoCrearActualizarDtoValidator : AbstractValidator<ProductoCrearActualizarDto>
{
    public ProductoCrearActualizarDtoValidator()
    {

        RuleFor(x => x.Nombre)
            .MinimumLength(3)
            .MaximumLength(256)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido");

        RuleFor(x => x.Precio)
                .NotNull()
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .GreaterThanOrEqualTo(0)
                .WithMessage("El valor ingresado no es correcto. Debe ser un valor mayor o igual a {ComparisonValue}. El valor actual es {PropertyValue}");

        RuleFor(x => x.Observaciones)
                .MaximumLength(256)
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
                .NotNull().
                Matches(@"^[A-Za-z0-9\s]+$").WithMessage("Solo se acepta letras y números");

        RuleFor(x => x.Caducidad)
                .NotNull()
                .NotEmpty().WithMessage("El campo {PropertyName} es requerido");
                
        RuleFor(x => x.MarcaId)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .Length(8).WithMessage("Id de la Marca es incorrecto");

        RuleFor(x => x.TipoProductoId)
            .NotNull()
            .NotEmpty().WithMessage("El campo {PropertyName} es requerido")
            .Length(8).WithMessage("Id del Tipo de Producto es incorrecto"); 
    }
}