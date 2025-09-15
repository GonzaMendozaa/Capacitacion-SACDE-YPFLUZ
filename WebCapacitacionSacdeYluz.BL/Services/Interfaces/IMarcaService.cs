using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.BL.Services.Interfaces
{
    public interface IMarcaService
    {
        public List<DwdMarca> GetAllMarcas();
        public DwdMarca CrearMarca(DwdMarca marca);
        public DwdMarca UpdateMarca(DwdMarca marca);
        public void DeleteMarca(int Id);

    }
}
