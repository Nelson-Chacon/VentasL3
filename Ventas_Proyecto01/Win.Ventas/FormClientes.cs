using BL.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Ventas
{
    public partial class FormClientes : Form
    {

        ClientesBL _clienteBL;

        public FormClientes()
        {

            InitializeComponent();
            _clienteBL = new ClientesBL();
            clientesBindingSource.DataSource = _clienteBL.ObtenerClientes();
        }

        private void clientesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            clientesBindingSource.EndEdit(); //finaliza edicion
            var clientes = (Clientes)clientesBindingSource.Current;//Obtenemos las propiedades del producto actual de la clase Niños
            
            var resultadoClientes = _clienteBL.GuardarClientes(clientes);

            if (resultadoClientes.Exitoso == true)
            {
                clientesBindingSource.ResetBindings(false);//Guarda los nuevos cambios en la lista
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("El cliente ha sido guardado", "Mensaje de confirmacion");
            }
            else
            {
                MessageBox.Show(resultadoClientes.Mensaje);
            }
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _clienteBL.AgregarCliente();
            clientesBindingSource.MoveLast();
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
                var resultadoCliente = MessageBox.Show("Desea eliminar este registro?", "Eliminar", MessageBoxButtons.YesNo);
                if (resultadoCliente == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Eliminar(id);
                }

            }
        }

        private void Eliminar(int id)
        {
            var resultado = _clienteBL.EliminarClientes(id);

            if (resultado == true)
            {
                clientesBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el Cliente");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _clienteBL.CancelarCambios();
            DeshabilitarHabilitarBotones(true);
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {

        }
    }
}
