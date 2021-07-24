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
    public partial class FormMujer : Form
    {
        MujeresBL _producto;
        SeccionBL _seccionBL;

        public FormMujer()
        {
            InitializeComponent();
            _producto = new MujeresBL();//Inicializar
            mujerBindingSource.DataSource = _producto.obtenerProductos();

            _seccionBL = new SeccionBL();
            listaSeccionBindingSource.DataSource = _seccionBL.ObtenerSeccion();
        }

        private void Form2Mujer_Load(object sender, EventArgs e)
        {

        }

        private void mujerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            mujerBindingSource.EndEdit(); //finaliza edicion
            var mujer = (Mujer)mujerBindingSource.Current;//Obtenemos las propiedades del producto actual de la clase Niños

            if (fotoPictureBox.Image != null)
            {
                mujer.Foto = Program.imageToByteArray(fotoPictureBox.Image);
            }
            else
            {
                mujer.Foto = null;
            }


            var resultadoMujer = _producto.GuardarProdMujeres(mujer);

            if (resultadoMujer.Exitoso == true)
            {
                mujerBindingSource.ResetBindings(false);//Guarda los nuevos cambios en la lista
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Elemento Guardado", "Mensaje de confirmacion");
            }
            else
            {
                MessageBox.Show(resultadoMujer.Mensaje);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _producto.AgregarProdMujeres();
            mujerBindingSource.MoveLast();
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
            toolStripButton1.Visible = !valor;

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

          if (idTextBox.Text != "")//Para poder eliminar las listas deben  contener valores
            {
                var resultadoMujer = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultadoMujer == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Eliminar(id);
                }

            }
        }
        private void Eliminar(int id)
        {

            var resultadoNiños = _producto.EliminarProdMujeres(id);

            if (resultadoNiños == true)
            {
                mujerBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error eliminar al producto");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _producto.CancelarCambios();
            DeshabilitarHabilitarBotones(true);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var mujer = (Mujer)mujerBindingSource.Current;

            if (mujer != null)
            {
                openFileDialog1.ShowDialog();
                var archivo = openFileDialog1.FileName;

                if (archivo != "")
                {
                    var fileInfo = new FileInfo(archivo);

                    var fileStream = fileInfo.OpenRead();

                    fotoPictureBox.Image = Image.FromStream(fileStream);
                }
            }
            else
            {
                MessageBox.Show("Cree un producto antes de asignarle una imagen");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            fotoPictureBox.Image = null;
        }
    }
}
