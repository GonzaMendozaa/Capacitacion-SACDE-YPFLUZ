using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.BL.Services.Interfaces
{
    public interface IProveedorService
    {
        public List<DwdProveedor> GetAllProveedores();
        public DwdProveedor CrearProveedor(DwdProveedor prov);
        public DwdProveedor ActualizarProveedor(DwdProveedor prov);
        public bool EliminarProveedor(int provId);
    }
}
