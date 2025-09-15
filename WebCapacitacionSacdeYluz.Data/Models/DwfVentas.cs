using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCapacitacionSacdeYluz.Data.Models
{
    public class DwfVentas
    {
        public int Id {  get; set; }
        public DateTime FechaPago { get; set; }

        //Relacion Con Vendedor
        public DwdVendedor? Vendedor { get; set; }
        public int VendedorId { get; set; }

       
    }
}
