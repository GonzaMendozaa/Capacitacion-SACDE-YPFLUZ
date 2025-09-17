using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCapacitacionSacdeYluz.Data.Models.DTOS
{
    public class CrearVentaDTO
    {
        public int VendedorId { get; set; }
        public List<VentaXCalzadoDTO> Calzados { get; set; } = new List<VentaXCalzadoDTO>();
    }
}
