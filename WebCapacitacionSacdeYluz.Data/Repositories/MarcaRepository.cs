using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

namespace WebCapacitacionSacdeYluz.Data.Repositories.Interfaces;

public class MarcaRepository : IMarcaRepository
{
    private readonly WebCapacitacionSacdeLuzDbContext _context;

    public MarcaRepository(WebCapacitacionSacdeLuzDbContext context)
    {
        _context = context;
    }

    public List<DwdMarca> GetAllMarca()
    {
        return _context.DwdMarca
            .ToList();
    }

    #region Post
    public DwdMarca CrearMarca(DwdMarca marca)
    {
        try
        {
            _context.DwdMarca.Add(marca);
            _context.SaveChanges();
            return marca;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void DeleteMarca(int Id)
    {
        try
        {
            var marca = _context.DwdMarca.Where(x => x.Id == Id).FirstOrDefault();
            _context.DwdMarca.Remove(marca);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Put
    public DwdMarca UpdateMarca(DwdMarca marca)
    {
        try
        {
            _context.DwdMarca.Update(marca);
            _context.SaveChanges();
            return marca;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public List<DwdMarca> GetAllMarcas()
    {
        var marcas = _context.DwdMarca
                .ToList();
        return marcas;
    }
    #endregion
}



