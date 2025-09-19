using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.Data.Repositories.Interfaces
{
    public interface ICompraRepository
    {
        public List<DwfCompra> GetAllCompras();
        public DwfCompra CrearCompra(DwfCompra compra);

    }
}
