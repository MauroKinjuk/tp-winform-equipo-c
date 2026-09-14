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

            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            

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
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

            frmDetalleArticulo detalle = new frmDetalleArticulo(seleccionado);
            detalle.ShowDialog();
        }
    }
}
