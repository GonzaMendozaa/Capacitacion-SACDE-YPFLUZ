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
    public class TiendaService : ITiendaService
    {
        private readonly ITiendaRepository _tiendaRepository;

        public TiendaService(ITiendaRepository tiendaRepository)
        {
            _tiendaRepository = tiendaRepository;
        }

        public List<DwdTienda> GetAllTiendas()
        {
            return _tiendaRepository.GetAllTiendas();
        }

        public DwdTienda CrearTienda(DwdTienda tienda)
        {
            try
            {
                return _tiendaRepository.CrearTienda(tienda);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DwdTienda UpdateTienda(DwdTienda tienda)
        {
            try
            {
                return _tiendaRepository.UpdateTienda(tienda);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteTienda(int id)
        {
            try
            {
                _tiendaRepository.DeleteTienda(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}

