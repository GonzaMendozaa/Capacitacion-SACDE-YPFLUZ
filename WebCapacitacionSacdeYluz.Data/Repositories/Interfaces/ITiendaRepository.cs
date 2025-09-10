using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.Data.Repositories.Interfaces
{
    public interface ITiendaRepository
    {
        public List<DwdTienda> GetAllTiendas();
        public DwdTienda CrearTienda (DwdTienda tienda);
        public DwdTienda UpdateTienda(DwdTienda tienda);
        public void DeleteTienda(int Id);
    }
}
