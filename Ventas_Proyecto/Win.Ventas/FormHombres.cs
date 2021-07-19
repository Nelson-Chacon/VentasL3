using BL.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Ventas
{
    public partial class FormHombres : Form
    {
        HombresBL _producto;

        SeccionBL _seccionBL;

        public FormHombres()
        {
            InitializeComponent();
            _producto = new HombresBL();
            hombreBindingSource.DataSource = _producto.obtener_Productos();

            _seccionBL = new SeccionBL();
            listaSeccionBindingSource.DataSource = _seccionBL.ObtenerSeccion();

        }

        private void hombreBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            hombreBindingSource.EndEdit();//finaliza edicion
            var hombre = (Hombre)hombreBindingSource.Current;//Obtenemos las propiedades del producto actual de la clase Hombres

            if (fotoPictureBox3.Image != null)
            {
                hombre.Foto = Program.imageToByteArray(fotoPictureBox3.Image);
            }
            else
            {
                hombre.Foto = null;
            }

            var resultado = _producto.GuardarProdHombres(hombre);

            if (resultado.Exitoso == true)
            {
                hombreBindingSource.ResetBindings(false);//Guarda los nuevos cambios en la lista
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Elemento Guardado", "Mensaje de confirmacion");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _producto.AgregarProdHombres();
            hombreBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
        }

        private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;

            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            toolStripButtonCancelar.Visible = !valor;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "")//Para poder eliminar las listas deben  contener valores
            {
                var resultado = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultado == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);

                    Eliminar(id);
                }
            }
        }
        private void Eliminar(int id)
        {
            var resultado = _producto.EliminarProdHombres(id);
            if (resultado == true)
            {
                hombreBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el producto");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _producto.CancelarCambios();
            DeshabilitarHabilitarBotones(true);
            
        }

        private void FormHomb_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var hombre = (Hombre)hombreBindingSource.Current;

            if (hombre != null)
            {
                openFileDialog3.ShowDialog();
                var archivo = openFileDialog3.FileName;

                if (archivo != "")
                {
                    var fileInfo = new FileInfo(archivo);

                    var fileStream = fileInfo.OpenRead();

                    fotoPictureBox3.Image = Image.FromStream(fileStream);
                }
            }
            else
            {
                MessageBox.Show("Cree un producto antes de asignarle una imagen");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            fotoPictureBox3.Image = null;
        }
    }
}
