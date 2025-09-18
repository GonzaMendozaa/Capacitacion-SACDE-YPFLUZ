using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.BL.Services.Interfaces
{
    public interface IVendedorService
    {
        public List<DwdVendedor> GetAllVendedores();
        public DwdVendedor CrearVendedor(DwdVendedor vendedor);
        public DwdVendedor UpdateVendedor(DwdVendedor vendedor);
        public void DeleteVendedor(int Id);

        public List<DwdVendedor> GetVendedoresByTienda(int tiendaId);
    }
}
