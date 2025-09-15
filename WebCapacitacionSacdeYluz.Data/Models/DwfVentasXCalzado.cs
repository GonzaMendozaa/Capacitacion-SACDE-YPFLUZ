using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCapacitacionSacdeYluz.Data.Models
{
    public class DwfVentasXCalzado
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }

        //Relacion con Calzado
        public DwdCalzado? Calzado { get; set; }
        public int CalzadoId { get; set; }

        //Relacion con Venta
        public DwfVentas? Ventas { get; set; }
        public int VentasId { get; set; }

    }
}
