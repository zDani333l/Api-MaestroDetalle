using Api_MaestroDetalle.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Api_MaestroDetalle.Filters
{
    public class ExceptionFilter: IExceptionFilter
    {
        //METODO PARA CAPTURAR LAS EXCEPCIONES QUE LANZAMOS CON LA CLASE 'ExceptionApi'
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(ExceptionApi))
            {
                var exception = (ExceptionApi)context.Exception;
                
                var validation = new
                {
                    Status = exception.StatusCode,
                    Title = "Bad Request",
                    Description = exception.Message
                };


                var json = new
                {
                    errors = new { validation }
                };

                
                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
        }
    }
}
