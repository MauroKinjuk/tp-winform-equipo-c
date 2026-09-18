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
    public partial class frmModificarCategoria : Form
    {
        Categoria categoria;

        public frmModificarCategoria(Categoria categoriaSeleccionada)
        {
            InitializeComponent();
            this.categoria = categoriaSeleccionada;
        }

        private void frmModificarCategoria_Load(object sender, EventArgs e)
        {
            txtbNombre.Text = categoria.Descripcion;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                this.categoria.Descripcion = txtbNombre.Text;

                negocio.modificar(this.categoria);
                this.Close();
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
