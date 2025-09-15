using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCapacitacionSacdeYluz.Data.Models
{
    public class DwdVendedor
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        
        //Relacion con Tienda
        public DwdTienda? Tienda { get; set; }
        public int TiendaId { get; set; }

    }
}
