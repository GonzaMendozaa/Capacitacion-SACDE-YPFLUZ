using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.Data.Repositories.Interfaces
{
    public interface IVendedorRepository
    {
        public List<DwdVendedor> GetAllVendedores();
        public DwdVendedor CrearVendedor(DwdVendedor vendedor);
        public DwdVendedor UpdateVendedor(DwdVendedor vendedor);
        public void DeleteVendedor(int Id);

        public List<DwdVendedor> GetVendedoresByTienda(int tiendaId);
    }
}
