using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Dominio;
using Negocio;
using System.Text.RegularExpressions;

namespace Presentacion
{
    public partial class Artículo_en_Destalle : Form
    {
        AccesoDatos datos = new AccesoDatos();
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        private Articulo articulo;
        public Artículo_en_Destalle(Articulo elegido)
        {
            InitializeComponent();
            articulo = elegido;
            Text = "Artículo en Detalle";
        }

        private void Artículo_en_Destalle_Load(object sender, EventArgs e)
        {
            MarcaNegocio marca = new MarcaNegocio();
            CategoriaNegocio categoria = new CategoriaNegocio();

            cobMarca.DataSource = marca.listar();
            cobMarca.ValueMember = "Id";
            cobMarca.DisplayMember = "Descripcion";
            cobCategoria.DataSource = categoria.listar();
            cobCategoria.ValueMember = "Id";
            cobCategoria.DisplayMember = "Descripcion";

            txtCodigo.Text = articulo.Codigo.ToString();
            txtNombre.Text = articulo.Nombre;
            txtDescripcion.Text = articulo.Descripcion;
            cargarImagen(articulo.ImagenUrl);
            cobMarca.SelectedValue = articulo.Marca.Id;
            cobCategoria.SelectedValue = articulo.Tipo.Id;
            txtPrecio.Text = articulo.Precio.ToString();
        }

        public void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
            }
        }
    }
}