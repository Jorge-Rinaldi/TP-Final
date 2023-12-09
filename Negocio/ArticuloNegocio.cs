using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using System.Net;
using System.Security.Policy;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            conexion.ConnectionString= "server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "select Codigo, Nombre, Descripcion, ImagenUrl, Precio from ARTICULOS";
            comando.Connection = conexion;

            conexion.Open();
            lector = comando.ExecuteReader();

            while (lector.Read())
            {
                Articulo art = new Articulo();
                art.Codigo = (string)lector["Codigo"];
                art.Nombre = (string)lector["Nombre"];
                art.Descripcion = (string)lector["Descripcion"];
                art.Imagen = (string)lector["ImagenUrl"];
                art.Precio = (decimal)lector["Precio"];

                lista.Add(art);
            }

            conexion.Close();
            return lista;
        }
    }
}
