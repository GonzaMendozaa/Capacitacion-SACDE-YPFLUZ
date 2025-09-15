using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.Data.Repositories.Interfaces
{
    public interface IMarcaRepository
    {
        public List<DwdMarca> GetAllMarcas();
        public DwdMarca CrearMarca(DwdMarca tienda);
        public DwdMarca UpdateMarca(DwdMarca tienda);
        public void DeleteMarca(int Id);
    }
}
