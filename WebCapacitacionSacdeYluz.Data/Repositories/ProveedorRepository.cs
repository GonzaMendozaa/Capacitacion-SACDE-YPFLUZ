using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

namespace WebCapacitacionSacdeYluz.Data.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly WebCapacitacionSacdeLuzDbContext _context;

        public ProveedorRepository(WebCapacitacionSacdeLuzDbContext context)
        {
            _context = context;
        }

        public DwdProveedor ActualizarProveedor(DwdProveedor prov)
        {
            _context.DwdProveedor.Update(prov);
            _context.SaveChanges();
            return prov;
        }

        public DwdProveedor CrearProveedor(DwdProveedor prov)
        {
            _context.DwdProveedor.Add(prov);
            _context.SaveChanges();
            return prov;
        }

        public bool EliminarProveedor(int provId)
        {
            DwdProveedor prov = _context.DwdProveedor.Find(provId);
            _context.Remove(prov);
            _context.SaveChanges();
            return true;
        }

        public List<DwdProveedor> GetAllProveedores()
        {
            return _context.DwdProveedor.ToList();
        }
    }
}
