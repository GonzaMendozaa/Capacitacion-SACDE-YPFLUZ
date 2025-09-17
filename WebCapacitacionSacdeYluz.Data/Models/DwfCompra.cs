using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCapacitacionSacdeYluz.Data.Models
{
    public class DwfCompra
    {
        public int Id { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.Now;

        public double TotalCompra { get; set; }

        public double Ganancia { get; set; }

        //Relacion con Proveedor
        public DwdProveedor? Proveedor { get; set; }
        public int ProveedorId { get; set; }

        //Relacion con Tienda
        public DwdTienda? Tienda { get; set; }
        public int TiendaId { get; set; }
    }
}
