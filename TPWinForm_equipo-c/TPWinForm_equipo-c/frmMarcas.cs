using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace TPWinForm_equipo_c
{
    public partial class frmMarcas : Form
    {

        private List<Marca> listaMarcas;

        public frmMarcas()
        {
            InitializeComponent();
        }
        private void frmMarcas_Load(object sender, EventArgs e)
        {
            dgvMarcas.AutoGenerateColumns = true;

            cargarMarcas();
        }

        private void cargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                listaMarcas = negocio.listar();
                dgvMarcas.DataSource = listaMarcas;

                dgvMarcas.Columns["Id"].Visible = false;
                dgvMarcas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                Marca marca = new Marca();
                marca.Descripcion = txtbNombre.Text;

                negocio.agregar(marca);
                cargarMarcas();

                txtbNombre.Clear();
                txtbNombre.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
