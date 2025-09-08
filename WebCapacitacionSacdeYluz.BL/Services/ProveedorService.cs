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
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _proveedorRepository;
        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public DwdProveedor ActualizarProveedor(DwdProveedor prov)
        {
           return _proveedorRepository.ActualizarProveedor(prov);
        }

        public DwdProveedor CrearProveedor(DwdProveedor prov)
        {
           return _proveedorRepository.CrearProveedor(prov);
        }

        public bool EliminarProveedor(int provId)
        {
            return _proveedorRepository.EliminarProveedor(provId);
        }

        public List<DwdProveedor> GetAllProveedores()
        {
            return _proveedorRepository.GetAllProveedores();
        }
    }
}
