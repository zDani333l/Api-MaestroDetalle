using Api_MaestroDetalle.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Api_MaestroDetalle.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        //METODO QUE SE EJECUTA ANTES DE LA EJECUCION DE CUALQUIER ACCION (CONTROLADOR)
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);

                throw new ExceptionApi("Modelo invalido",404);
               
            }

            await next();
        }
    }
}
