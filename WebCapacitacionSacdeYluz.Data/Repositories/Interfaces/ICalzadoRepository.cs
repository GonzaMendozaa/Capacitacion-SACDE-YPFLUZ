using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.Data.Repositories.Interfaces
{
    public interface ICalzadoRepository
    {
        public List<DwdCalzado> GetAllCalzados();
        public DwdCalzado CrearCalzado(DwdCalzado calzado);
        public DwdCalzado UpdateCalzado(DwdCalzado calzado);
        public void DeleteCalzado(int Id);

    }
}
