using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

namespace WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

public class CompraRepository : ICompraRepository
{
    private readonly WebCapacitacionSacdeLuzDbContext _context;

    public CompraRepository(WebCapacitacionSacdeLuzDbContext context)
    {
        _context = context;
    }

    public List<DwfCompra> GetAllCompras()
    {
        return _context.DwfCompra
            .Include (c=>c.Tienda)
            .Include(c=>c.Proveedor)
            .ToList();
    }

    #region Post
    public DwfCompra CrearCompra(DwfCompra compra)
    {
        try
        {
            _context.DwfCompra.Add(compra);
            _context.SaveChanges();
            return compra;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void DeleteCompra(int Id)
    {
        try
        {
            var compra = _context.DwfCompra.Where(x => x.Id == Id).FirstOrDefault();
            _context.DwfCompra.Remove(compra);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Put
    public DwfCompra UpdateCompra(DwfCompra compra)
    {
        try
        {
            _context.DwfCompra.Update(compra);
            _context.SaveChanges();
            return compra;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion
}


