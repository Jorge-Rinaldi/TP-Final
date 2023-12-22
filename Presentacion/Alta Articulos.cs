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
using static System.Net.Mime.MediaTypeNames;

namespace Presentacion
{
    public partial class frmAltaArticulos : Form
    {
        AccesoDatos datos = new AccesoDatos();
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public frmAltaArticulos()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo art = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();
            
            try
            {
                art.Codigo = txtCodigo.Text;
                art.Nombre = txtNombre.Text;
                art.Descripcion = txtDescripcion.Text;
                art.ImagenUrl = txtUrlImagen.Text;
                art.Precio = int.Parse(txtPrecio.Text);
                art.Tipo = (Categoria)cobCategoria.SelectedItem;
                art.Marca = (Marca)cobMarca.SelectedItem;

                negocio.agregar(art);
                MessageBox.Show("Agregado exitosamente");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaArticulos_Load(object sender, EventArgs e)
        {
            MarcaNegocio mark = new MarcaNegocio();
            CategoriaNegocio cat = new CategoriaNegocio();
            try
            {
                cobMarca.DataSource = mark.listar();
                cobCategoria.DataSource = cat.listar();
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.ToString());
            }
           
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxUrlImagen.Load(imagen);
            }
            catch (Exception)
            {
                pbxUrlImagen.Load("https://img.freepik.com/vector-premium/icono-marco-fotos-foto-vacia-blanco-vector-sobre-fondo-transparente-aislado-eps-10_399089-1290.jpg");
            }
        }
    }
}
