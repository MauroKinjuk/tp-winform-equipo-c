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
    public partial class frmModificarMarca : Form
    {
        Marca marca;

        public frmModificarMarca(Marca marcaSeleccionada)
        {
            InitializeComponent();
            this.marca = marcaSeleccionada;
        }

        private void frmModificarMarca_Load(object sender, EventArgs e)
        {
            txtbNombre.Text = marca.Descripcion;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                this.marca.Descripcion = txtbNombre.Text;

                negocio.modificar(this.marca);
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
