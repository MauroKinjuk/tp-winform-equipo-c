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
    public partial class frmCategorias : Form
    {

        private List<Categoria> listaCategorias;

        public frmCategorias()
        {
            InitializeComponent();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            dgvCategorias.AutoGenerateColumns = true;

            cargarCategorias();
        }

        private void cargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                listaCategorias = negocio.listar();
                dgvCategorias.DataSource = listaCategorias;

                dgvCategorias.Columns["Id"].Visible = false;
                dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                Categoria categoria = new Categoria();
                categoria.Descripcion = txtbNombre.Text;

                negocio.agregar(categoria);
                cargarCategorias();

                txtbNombre.Clear();
                txtbNombre.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Categoria categoriaSeleccionada = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;

            frmModificarCategoria modCategoria = new frmModificarCategoria(categoriaSeleccionada);
            modCategoria.ShowDialog();

            cargarCategorias();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria categoriaSeleccionada = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;

            try
            {
                DialogResult respuesta = MessageBox.Show(this, "Desea eliminar '" + categoriaSeleccionada.Descripcion + "'?",
                "Eliminando",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(categoriaSeleccionada.Id);
                    cargarCategorias();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
