using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

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

            try
            {
                conexion.ConnectionString = ("server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=TRUE");
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = ("select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Tipo, ImagenUrl, Precio, A.IdMarca, A.IdCategoria from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id AND A.IdCategoria = C.Id");
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.Id = (int)lector["Id"];
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.Marca.Id = (int)lector["IdMarca"];
                    aux.Marca.Descripcion = (string)lector["Marca"];
                    aux.ImagenUrl = (string)lector["ImagenUrl"];
                    aux.Precio = (decimal)lector["Precio"];
                    aux.Tipo = new Categoria();
                    aux.Tipo.Id = (int)lector["IdCategoria"];
                    aux.Tipo.Descripcion = (string)lector["Tipo"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values ('"+nuevo.Codigo+"', '"+nuevo.Nombre+"', '"+nuevo.Descripcion+"', @IdMarca, @IdCategoria, '"+nuevo.ImagenUrl+"', "+nuevo.Precio+")");
                datos.setearParametros("@IdMarca", nuevo.Marca.Id);
                datos.setearParametros("@IdCategoria", nuevo.Tipo.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public void modificar(Articulo modificado)
        { 
            AccesoDatos datos = new AccesoDatos();  
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id");
                datos.setearParametros("@Codigo", modificado.Codigo);
                datos.setearParametros("@Nombre", modificado.Nombre);
                datos.setearParametros("@Descripcion", modificado.Descripcion);
                datos.setearParametros("@IdMarca", modificado.Marca.Id);
                datos.setearParametros("@IdCategoria", modificado.Tipo.Id);
                datos.setearParametros("@ImagenUrl", modificado.ImagenUrl);
                datos.setearParametros("@Precio", modificado.Precio);
                datos.setearParametros("@Id", modificado.Id);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
