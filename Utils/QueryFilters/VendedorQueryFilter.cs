namespace Api_MaestroDetalle.Utils.QueryFilters
{
    //CLASE MODELO PARA EL FILTRO Y PAGINACION LOS DATOS DEL MODELO VENDEDOR
    public class VendedorQueryFilter
    {
        public int? Id { get; set; }
        public string Nombre  { get; set; }
        public string Apellido { get; set; }
        public string NumeroIdentificación { get; set; }
        public int? CiudadId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
