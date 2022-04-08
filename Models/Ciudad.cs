using System;
using System.Collections.Generic;

#nullable disable

namespace Api_MaestroDetalle.Models
{
    public partial class Ciudad: BaseEntity
    {
        public Ciudad()
        {
            Vendedor = new HashSet<Vendedor>();
        }


        public string Descripcion { get; set; }

        public virtual ICollection<Vendedor> Vendedor { get; set; }
    }
}
