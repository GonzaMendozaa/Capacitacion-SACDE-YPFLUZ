using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCapacitacionSacdeYluz.Data.Models.DTOS
{
    public class StockCalzadoDTO
    {
            public int CalzadoId { get; set; }
            public decimal Talle { get; set; }
            public string? Modelo { get; set; }
            public string? Marca { get; set; }
            public decimal Stock { get; set; }
        
    }
}




