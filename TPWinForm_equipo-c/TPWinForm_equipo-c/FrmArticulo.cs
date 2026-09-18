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
    public partial class FrmArticulo : Form
    {
        private Articulo articulo = null;
        private List<Imagen> imagenes = new List<Imagen>();
        private int indiceImagen = 0;

        public FrmArticulo()
        {
            InitializeComponent();
        }
        
        public FrmArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (txtUrlImagen.Text.Trim() == "")
                return;

            Imagen img = new Imagen();
            img.ImagenUrl = txtUrlImagen.Text.Trim();
            imagenes.Add(img);
            indiceImagen = imagenes.Count - 1;
            mostrarImagen();
            txtUrlImagen.Clear();
        }

        private void btnQuitarImagen_Click(object sender, EventArgs e)
        {
            if (imagenes.Count > 0) { 
                imagenes.RemoveAt(indiceImagen);
                if (indiceImagen >= imagenes.Count)
                    indiceImagen = imagenes.Count - 1;
                mostrarImagen();
            }
        }

        private void pbxImagen_Click(object sender, EventArgs e)
        {

        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (indiceImagen > 0)
            {
                indiceImagen--;
                mostrarImagen();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (indiceImagen < imagenes.Count - 1)
            {
                indiceImagen++;
                mostrarImagen();
            }
        }

        private void mostrarImagen()
        {
            try
            {
                if (imagenes.Count > 0)
                    pictureBox1.Load(imagenes[indiceImagen].ImagenUrl);
                else
                    pictureBox1.Image = null;
            }
            catch (Exception)
            {
                pictureBox1.Image = null;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!validarCampos())
                return;
            ArticuloNegocio negocio = new ArticuloNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio(); ;
            try { 
                if (articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);
                articulo.Marca = (Marca)comboMarca.SelectedItem;
                articulo.Categoria = (Categoria)comboCategoria.SelectedItem;

                if (articulo.Id == 0) {
                    articulo.Id = negocio.agregar(articulo);
                    foreach (Imagen img in imagenes)
                    {
                        img.IdArticulo = articulo.Id;
                        imagenNegocio.agregar(img);
                    }
                    MessageBox.Show("Artículo agregado correctamente");
                }
                else { 
                    negocio.modificar(articulo);
                    imagenNegocio.eliminarPorArticulo(articulo.Id);
                    foreach (Imagen img in imagenes)
                    {
                        img.IdArticulo = articulo.Id;
                        imagenNegocio.agregar(img);
                    }
                    MessageBox.Show("Artículo modificado correctamente");
                }
                Close();
            }   
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool validarCampos() {
            if (txtCodigo.Text.Trim() == "" || txtNombre.Text.Trim() == "") {
                MessageBox.Show("Codigo y nombre son obligatorios");
                return false;
            }
            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Precio debe ser un número válido");
                return false;
            }
            return true;
        }

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio negocioMarca = new MarcaNegocio();
            CategoriaNegocio negocioCategoria = new CategoriaNegocio();
            try {
                comboMarca.DataSource = negocioMarca.listar();
                comboMarca.ValueMember = "Id";
                comboMarca.DisplayMember = "Descripcion";

                comboCategoria.DataSource = negocioCategoria.listar();
                comboCategoria.ValueMember = "Id";
                comboCategoria.DisplayMember = "Descripcion";

                if (articulo != null) { 
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    comboMarca.SelectedValue = articulo.Marca.Id;
                    comboCategoria.SelectedValue = articulo.Categoria.Id;

                    ImagenNegocio imagenNegocio = new ImagenNegocio();
                    imagenes = imagenNegocio.listarPorArticulo(articulo.Id);
                    mostrarImagen();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
