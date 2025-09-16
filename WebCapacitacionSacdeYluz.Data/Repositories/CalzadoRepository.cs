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
    public class CalzadoRepository : ICalzadoRepository
    {
        private readonly WebCapacitacionSacdeLuzDbContext _context;

        public CalzadoRepository(WebCapacitacionSacdeLuzDbContext context)
        {
            _context = context;
        }

        public List<DwdCalzado> GetAllCalzados()
        {
            return _context.DwdCalzado
                .Include(m => m.Marca)
                .ToList();
        }

        #region Post
        public DwdCalzado CrearCalzado(DwdCalzado calzado)
        {
            try
            {
                _context.DwdCalzado.Add(calzado);
                _context.SaveChanges();
                return calzado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCalzado(int Id)
        {
            try
            {
                var calzado = _context.DwdCalzado.Where(x => x.Id == Id).FirstOrDefault();
                _context.DwdCalzado.Remove(calzado);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Put
        public DwdCalzado UpdateCalzado(DwdCalzado calzado)
        {
            try
            {
                _context.DwdCalzado.Update(calzado);
                _context.SaveChanges();
                return calzado;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
