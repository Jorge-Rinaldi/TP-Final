using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulo;
        public Form1()
        {
            InitializeComponent();
            Text = "Lista Articulos";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();   
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
        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulo = negocio.listar();
                dgvArticulo.DataSource = listaArticulo;
                cargarImagen(listaArticulo[0].ImagenUrl);
                dgvArticulo.Columns["ImagenUrl"].Visible = false;
                dgvArticulo.Columns["Id"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
       
        private void dgvArticulo_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulo.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }
           
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulos alta = new frmAltaArticulos();
            alta.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos();
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            try
            {
                datos.setearConsulta("delete from ARTICULOS where Id = @Id");
                datos.setearParametros("@Id", seleccionado.Id);
                datos.ejecutarAccion();
                MessageBox.Show("Eliminado Exitosamente");
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { datos.cerrarConexion(); }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            if(dgvArticulo.CurrentRow != null)
            {
                seleccionado = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
                frmAltaArticulos modificar = new frmAltaArticulos(seleccionado);
                modificar.ShowDialog();
                cargar();
            }
            else
            {
                MessageBox.Show("Por favor seleccione un artículo");
            }
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<Articulo> listaFiltrada;
            listaFiltrada = listaArticulo.FindAll(articulo => articulo.Codigo.ToLower().Contains(txtBuscar.Text.ToLower()) || articulo.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())|| articulo.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower()) || articulo.Marca.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower()) || articulo.Tipo.Descripcion.ToLower().Contains(txtBuscar.Text.ToLower()));
            dgvArticulo.DataSource = null;
            dgvArticulo.DataSource = listaFiltrada;
            dgvArticulo.Columns["ImagenUrl"].Visible = false;
            dgvArticulo.Columns["Id"].Visible = false;
        }

        private void dgvArticulo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Articulo elegido;
            elegido = (Articulo)dgvArticulo.CurrentRow.DataBoundItem;
            Artículo_en_Destalle mostrar = new Artículo_en_Destalle(elegido);
            mostrar.ShowDialog();
        }
    }
}
