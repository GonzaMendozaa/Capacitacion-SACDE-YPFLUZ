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
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _MarcaRepository;

        public MarcaService(IMarcaRepository MarcaRepository)
        {
            _MarcaRepository = MarcaRepository;
        }

        public List<DwdMarca> GetAllMarcas()
        {
            return _MarcaRepository.GetAllMarcas();
        }

        public DwdMarca CrearMarca(DwdMarca Marca)
        {
            try
            {
                return _MarcaRepository.CrearMarca(Marca);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DwdMarca UpdateMarca(DwdMarca Marca)
        {
            try
            {
                return _MarcaRepository.UpdateMarca(Marca);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteMarca(int id)
        {
            try
            {
                _MarcaRepository.DeleteMarca(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}


