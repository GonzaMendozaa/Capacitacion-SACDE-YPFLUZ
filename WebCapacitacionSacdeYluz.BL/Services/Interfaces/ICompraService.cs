using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.BL.Services.Interfaces
{
    public interface ICompraService
    {
        public List<DwfCompra> GetAllCompras();
        public DwfCompra CrearCompra(DwfCompra compra);

    }
}

