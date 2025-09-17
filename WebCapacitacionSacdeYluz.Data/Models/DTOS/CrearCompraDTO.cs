using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCapacitacionSacdeYluz.Data.Models.DTOS
{
    public class CrearCompraDTO
    {
        public int TiendaId { get; set; }
        public int ProveedorId { get; set; }
        public List<CompraCalzadoDTO> Calzados { get; set; } = new();
    }
}




