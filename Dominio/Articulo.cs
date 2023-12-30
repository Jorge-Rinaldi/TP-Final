using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public Articulo()
        {
            Marca = new Marca();
        }

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Tipo { get; set; }

        [DisplayName("Imagen")]
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }
    }
}
