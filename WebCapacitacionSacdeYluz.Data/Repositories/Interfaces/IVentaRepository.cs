using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.Data.Repositories.Interfaces
{
    public interface IVentaRepository
    {
        List<DwfVentas> GetAllVentas();
        DwfVentas CrearVenta(DwfVentas venta);

        DwdVendedor? GetVendedorById(int vendedorId);
        DwdCalzado? GetCalzadoById(int calzadoId);
        DwfTiendaXCalzado? GetStock(int tiendaId, int calzadoId);
        DwfVentasXCalzado AddVentaXCalzado(DwfVentasXCalzado ventaXCalzado);
        void SaveChanges();
    }

}
