using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCapacitacionSacdeYluz.Data.Models
{
    public class DwfTiendaXCalzado
    {
        public int Id { get; set; }
        public int Stock { get; set; }

        //Relacion con Tienda
        public DwdTienda Tienda { get; set; }
        public int TiendaId { get; set; }

        //Relacion con Calzado
        public DwdCalzado Calzado { get; set; }
        public int CalzadoId { get; set; }

    }
}
