using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TPWinForm_equipo_c
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulos;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            dgvArticulos.AutoGenerateColumns = true;

            listaArticulos = negocio.listar();
            dgvArticulos.DataSource = listaArticulos;

            List<string> marcas = listaArticulos
                .Select(x => x.Marca.Descripcion)
                .Distinct()
                .ToList();

            marcas.Insert(0, "Todas");

            cboMarca.DataSource = marcas;

            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            
            
            cboCampo.Items.Add("Código");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripción");
            cboCampo.Items.Add("Marca");
            cboCampo.Items.Add("Categoría");
            cboCampo.Items.Add("Precio");


            cboCampo.SelectedIndex = 0;
            cboCriterio.SelectedIndex = 0;
        }

      
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBuscar.Text;

            List<Articulo> listaFiltrada;

            if (filtro.Length >= 3)
            {
                listaFiltrada = listaArticulos.FindAll(x =>
                x.Nombre.ToUpper().Contains(filtro.ToUpper()) ||
                x.Codigo.ToUpper().Contains(filtro.ToUpper()) ||
                x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) ||
                x.Categoria.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrada = listaArticulos;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {

            if (dgvArticulos.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar un artículo");
                return;
            }

            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmDetalleArticulo detalle = new frmDetalleArticulo(seleccionado);
            detalle.ShowDialog();
        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            string marcaSeleccionada = cboMarca.SelectedItem.ToString();

            if (marcaSeleccionada == "Todas")
            {
                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = listaArticulos;
            }
            else
            {
                List<Articulo> listaFiltrada = listaArticulos.FindAll(
                    x => x.Marca.Descripcion == marcaSeleccionada
                );

                dgvArticulos.DataSource = null;
                dgvArticulos.DataSource = listaFiltrada;
            }

            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboCriterio.Items.Clear();

            if (cboCampo.SelectedItem.ToString() == "Precio")
            {
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {

            if (cboCampo.SelectedItem == null || cboCriterio.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un campo y un criterio");
                return;
            }

            if (txtFiltroAvanzado.Text == "")
            {
                MessageBox.Show("Debe ingresar un filtro");
                return;
            }

            if (cboCampo.SelectedItem.ToString() == "Precio")
            {
                decimal precio;

                if (!decimal.TryParse(txtFiltroAvanzado.Text, out precio))
                {
                    MessageBox.Show("Debe ingresar un precio válido");
                    return;
                }
            }

            ArticuloNegocio negocio = new ArticuloNegocio();

            string campo = cboCampo.SelectedItem.ToString();
            string criterio = cboCriterio.SelectedItem.ToString();
            string filtro = txtFiltroAvanzado.Text;

            dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);

            dgvArticulos.Columns["Id"].Visible = false;
        }

        private void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            txtFiltroAvanzado.Text = "";

            cboCampo.SelectedIndex = 0;
            cboCriterio.SelectedIndex = 0;

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaArticulos;

            dgvArticulos.Columns["Id"].Visible = false;
        }
    }
}
