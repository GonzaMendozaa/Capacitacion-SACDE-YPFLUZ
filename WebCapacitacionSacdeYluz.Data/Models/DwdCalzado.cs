using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCapacitacionSacdeYluz.Data.Models
{
    public class DwdCalzado
    {
        public int Id { get; set; }
       
        public int Talle { get; set; }
        public string? Modelo {  get; set; }
        public double Precio { get; set; }

        //Relacion Con Marca
        public DwdMarca? Marca { get; set; }
        public int MarcaId { get; set; }

        //Soft delete
        public bool Activo { get; set; } = false;

    }
}
