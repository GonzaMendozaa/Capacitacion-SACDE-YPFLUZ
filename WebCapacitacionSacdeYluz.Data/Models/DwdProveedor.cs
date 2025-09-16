using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCapacitacionSacdeYluz.Data.Models
{
    public class DwdProveedor
    {
        public int Id { get; set; }

        [Required]
        public string? Descripcion { get; set; }
        public double Comision { get; set; }
    }
}
