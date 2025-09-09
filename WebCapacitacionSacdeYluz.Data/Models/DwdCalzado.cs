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
       
        public double Talle { get; set; }
        public string Modelo {  get; set; }
        public double Precio { get; set; }

        //Relacion Con Marca
        public DwdMarca Marca { get; set; }
        public int MarcaId { get; set; }


    }
}
