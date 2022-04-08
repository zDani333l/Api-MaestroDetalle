using System;

namespace Api_MaestroDetalle.Exceptions
{
    //CLASE QUE SE ENCARGA DE CAPTURAR LOS MENSAJES LANZAMOS EN LAS EXCEPCIONES CONTROLADAS DE LA API
    public class ExceptionApi: Exception
    {
        public int StatusCode { get; set; }
        public ExceptionApi()
        {

        }

        public ExceptionApi(string message, int statusCode) : base( message )
        {
            this.StatusCode = statusCode;
        }
       
    }
}
