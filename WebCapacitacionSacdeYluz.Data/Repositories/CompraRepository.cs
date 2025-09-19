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

    #endregion
}


