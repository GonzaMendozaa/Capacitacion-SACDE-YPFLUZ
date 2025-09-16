using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

namespace WebCapacitacionSacdeYluz.BL.Services
{
    public class VendedorService : IVendedorService
    {
        private readonly IVendedorRepository _vendedorRepository;
        public VendedorService(IVendedorRepository vendedorRepository)
        {
            _vendedorRepository = vendedorRepository;
        }

        public List<DwdVendedor> GetAllVendedores()
        {
            return _vendedorRepository.GetAllVendedores();
        }

        public DwdVendedor CrearVendedor(DwdVendedor vendedor)
        {
            try
            {
                return _vendedorRepository.CrearVendedor(vendedor);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DwdVendedor UpdateVendedor(DwdVendedor vendedor)
        {
            try
            {
                return _vendedorRepository.UpdateVendedor(vendedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteVendedor(int Id)
        {
            try
            {
                _vendedorRepository.DeleteVendedor(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
