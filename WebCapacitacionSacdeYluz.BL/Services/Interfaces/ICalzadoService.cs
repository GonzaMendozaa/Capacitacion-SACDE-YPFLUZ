using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.BL.Services.Interfaces
{
    public interface ICalzadoService
    {
        public List<DwdCalzado> GetAllCalzados();
        public DwdCalzado CrearCalzado(DwdCalzado Calzado);
        public DwdCalzado UpdateCalzado(DwdCalzado Calzado);
        public void DeleteCalzado(int Id);
        List<DwfTiendaXCalzado> GetCalzadosByTienda(int tiendaId);
        

    }
}
