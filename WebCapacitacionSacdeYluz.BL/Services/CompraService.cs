using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.BL.Services.Interfaces;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Repositories;
using WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

namespace WebCapacitacionSacdeYluz.BL.Services
{
    public class CompraService : ICompraService
    {
        private readonly ICompraRepository _compraRepository;

        public CompraService(ICompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }

        public List<DwfCompra> GetAllCompras()
        {
            return _compraRepository.GetAllCompras();
        }

        public DwfCompra CrearCompra(DwfCompra compra)
        {
            try
            {
                return _compraRepository.CrearCompra(compra);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }

}

