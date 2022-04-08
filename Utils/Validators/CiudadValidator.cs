using Api_MaestroDetalle.Models;
using Api_MaestroDetalle.Models.Entities;
using FluentValidation;

namespace Api_MaestroDetalle.Validators
{
    //CLASE QUE IMPLEMENTA EL PAQUETE NUGET DE FLUENTVALIDATION QUE NOS PERMITE GENERAR VALIDACIONES PERSONALIZADAS A CADA ATRIBUTO DE MI CLASE "ciudadDTO"
    public class CiudadValidator : AbstractValidator<CiudadDTO>
    {
        
        public CiudadValidator()
        {
            RuleFor(x => x.Descripcion).NotNull().NotEmpty().WithMessage("El nombre de la ciudad es requerido")
                .Length(1, 50).WithMessage("El nombre de la ciudad debe tener entre 1 y 50 caracteres");
        }
    }
    
}
