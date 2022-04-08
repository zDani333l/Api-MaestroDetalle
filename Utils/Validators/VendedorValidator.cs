using Api_MaestroDetalle.Models.Entities;
using FluentValidation;

namespace Api_MaestroDetalle.Validators
{
    //CLASE QUE IMPLEMENTA EL PAQUETE NUGET DE FLUENTVALIDATION QUE NOS PERMITE GENERAR VALIDACIONES PERSONALIZADAS A CADA ATRIBUTO DE MI CLASE "VendedorDTO"
    public class VendedorValidator : AbstractValidator<VendedorDTO>
    {
        public VendedorValidator(){
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es requerido");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("El apellido es requerido");
            RuleFor(x => x.NumeroIdentificacion).NotEmpty().WithMessage("El numero de identificación es requerido");
            RuleFor(x => x.IdCiudad).NotNull().WithMessage("La ciudad es requerida");
        }
    }
}
