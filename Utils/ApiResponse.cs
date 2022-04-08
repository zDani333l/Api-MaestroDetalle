using Api_MaestroDetalle.Models;

namespace Api_MaestroDetalle.Utils
{
    //CLASE BASE PARA LA ESTRUCTURA DE RESPUESTAS GENERICAS PARA LAS DIFERENTES PETICIONES HTTP
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public Metadata Meta { get; internal set; }
    }
}
