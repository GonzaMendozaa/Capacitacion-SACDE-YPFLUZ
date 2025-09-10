using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.BL.Services.Interfaces
{
    public interface ITiendaService 
    {
        public List<DwdTienda> GetAllTiendas();
        public DwdTienda CrearTienda(DwdTienda tienda);
        public DwdTienda UpdateTienda(DwdTienda tienda);
        public void DeleteTienda(int Id);

    }
}
