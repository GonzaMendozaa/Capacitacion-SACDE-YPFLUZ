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
    public class CalzadoService : ICalzadoService
    {
        private readonly ICalzadoRepository _calzadoRepository;
        public CalzadoService(ICalzadoRepository calzadoRepository)
        {
            _calzadoRepository = calzadoRepository;
        }

        public List<DwdCalzado> GetAllCalzados()
        {
            return _calzadoRepository.GetAllCalzados();
        }

        public DwdCalzado CrearCalzado(DwdCalzado calzado)
        {
            try
            {
                return _calzadoRepository.CrearCalzado(calzado);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DwdCalzado UpdateCalzado(DwdCalzado calzado)
        {
            try
            {
                return _calzadoRepository.UpdateCalzado(calzado);
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
                _calzadoRepository.DeleteCalzado(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DwfTiendaXCalzado> GetCalzadosByTienda(int tiendaId)
        {
            return _calzadoRepository.GetCalzadosByTienda(tiendaId);
        }

    }
}
