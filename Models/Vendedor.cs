using System;
using System.Collections.Generic;

#nullable disable

namespace Api_MaestroDetalle.Models
{
    public partial class Vendedor: BaseEntity
    {
        
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NumeroIdentificacion { get; set; }
        public int? IdCiudad { get; set; }

        public virtual Ciudad Ciudad { get; set; }
    }
}
