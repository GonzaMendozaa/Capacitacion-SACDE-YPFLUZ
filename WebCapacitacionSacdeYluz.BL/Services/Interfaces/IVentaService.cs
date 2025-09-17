using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;
using WebCapacitacionSacdeYluz.Data.Models.DTOS;

namespace WebCapacitacionSacdeYluz.BL.Services.Interfaces
{
    public interface IVentaService
    {
        int CrearVenta(CrearVentaDTO dto);           // lógica completa de venta
        public List<DwfVentas> GetAllVentas();          // devolver ventas ya mapeadas
        DwfVentas UpdateVenta(DwfVentas venta);      // opcional
        void DeleteVenta(int id);                    // opcional
    }

}

