using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

namespace WebCapacitacionSacdeYluz.Data.Repositories
{
    public class VendedorRepository : IVendedorRepository
    {
        private readonly WebCapacitacionSacdeLuzDbContext _context;

        public VendedorRepository(WebCapacitacionSacdeLuzDbContext context)
        {
            _context = context;
        }

        public List<DwdVendedor> GetAllVendedores()
        {
            return _context.DwdVendedor
                           .Include(v => v.Tienda) // acá sí, porque estás en EF
                           .ToList();
        }


        #region Post
        public DwdVendedor CrearVendedor(DwdVendedor vendedor)
        {
            try
            {
                if (vendedor.TiendaId == 0)
                    throw new ArgumentException("Debe seleccionar una tienda válida.");
                _context.DwdVendedor.Add(vendedor);
                _context.SaveChanges();
                return vendedor;
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
                var vendedor = _context.DwdVendedor.FirstOrDefault(x => x.Id == Id);
                if (vendedor != null)
                {
                    _context.DwdVendedor.Remove(vendedor);
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }



        #endregion

        #region Put
        public DwdVendedor UpdateVendedor(DwdVendedor vendedor)
        {
            try
            {
                _context.DwdVendedor.Update(vendedor);
                _context.SaveChanges();
                return vendedor;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        public List<DwdVendedor> GetVendedoresByTienda(int tiendaId)
        {
            return _context.DwdVendedor
                .Where(v => v.TiendaId == tiendaId)
                .ToList();
        }
    }
}
