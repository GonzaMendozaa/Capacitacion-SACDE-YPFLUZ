using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

namespace WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

public class TiendaRepository: ITiendaRepository
{
    private readonly WebCapacitacionSacdeLuzDbContext _context;

    public TiendaRepository(WebCapacitacionSacdeLuzDbContext context)
    {  
        _context = context; 
    }

    public List<DwdTienda> GetAllTienda()
    {
        return _context.DwdTienda
            .ToList();
    }

    #region Post
    public DwdTienda CrearTienda(DwdTienda tienda)
    {
        try
        {
            _context.DwdTienda.Add(tienda);
            _context.SaveChanges();
            return tienda;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void DeleteTienda(int Id)
    {
        try
        {
            var tienda = _context.DwdTienda.Where(x => x.Id == Id).FirstOrDefault();
            _context.DwdTienda.Remove(tienda);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Put
    public DwdTienda UpdateTienda(DwdTienda tienda)
    {
        try
        {
            _context.DwdTienda.Update(tienda);
            _context.SaveChanges();
            return tienda;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<DwdTienda> GetAllTiendas()
    {
        throw new NotImplementedException();
    }
    #endregion
}


