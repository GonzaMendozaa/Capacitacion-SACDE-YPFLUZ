using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCapacitacionSacdeYluz.Data.Models
{
    public class DwfCompraXCalzado
    {
        public int Id { get; set; }

        public int Cantidad { get; set; }

        //Relacion con Compra
        public DwfCompra? Compra { get; set; }
        public int CompraId { get; set; }

        //Relacion Con Calzado
        public DwdCalzado? Calzado { get; set; }
        public int CalzadoId { get; set; }

    }
}
