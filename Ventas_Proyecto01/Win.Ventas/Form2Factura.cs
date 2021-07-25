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
    public partial class Form2Factura : Form
    {
        FacturaBL  _facturaBL;
        ClientesBL _clientesBL;
        MujeresBL  _mujeresBL;
        NiñosBL    _niñosBL;

        public Form2Factura()
        {
            InitializeComponent();

            _facturaBL = new FacturaBL();
            listadeFacturasBindingSource.DataSource = _facturaBL.ObtenerFacturas();

            _clientesBL = new ClientesBL();
            listaClientesBindingSource.DataSource = _clientesBL.ObtenerClientes();

            _mujeresBL = new MujeresBL();
            listaProdMujeresBindingSource.DataSource = _mujeresBL.obtenerProductos();

            _niñosBL = new NiñosBL();
            listaProdNiñosBindingSource.DataSource = _niñosBL.ObtenerProductos();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _facturaBL.AgregarFactura();
            listadeFacturasBindingSource.MoveLast();

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

        private void listadeFacturasBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listadeFacturasBindingSource.EndEdit();//finaliza edicion

            var factura = (Factura)listadeFacturasBindingSource.Current;//Obtenemos las propiedades del producto actual de la clase Hombres

            var resultado = _facturaBL.GuardarFactura(factura);

            if (resultado.Exitoso == true)
            {
                listadeFacturasBindingSource.ResetBindings(false);//Guarda los nuevos cambios en la lista
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Factura Guardada", "Mensaje de confirmacion");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarHabilitarBotones(true);
            _facturaBL.CancelarCambios();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var factura = (Factura)listadeFacturasBindingSource.Current;
            _facturaBL.AgregarFacturaDetalle(factura);
            DeshabilitarHabilitarBotones(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var factura = (Factura)listadeFacturasBindingSource.Current;
            var facturaDetalle = (FacturaDetalle)facturaDetalleBindingSource.Current;
            _facturaBL.RemoverFacturaDetalle(factura, facturaDetalle);
        }

        private void facturaDetalleDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void facturaDetalleDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var factura = (Factura)listadeFacturasBindingSource.Current;
            _facturaBL.CalcularFactura(factura);

            listadeFacturasBindingSource.ResetBindings(false);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != "")
            {
                var resultadoAnular = MessageBox.Show("Desea anular esta factura?", "Anular", MessageBoxButtons.YesNo);

                if (resultadoAnular == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    Anular(id);
                }
            }
        }

        private void Anular(int id)
        {
            var resultadoAnula = _facturaBL.AnularFactura(id);

            if (resultadoAnula == true)
            {
                listadeFacturasBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un erro al anular la factura");


            }
        }

        private void listadeFacturasBindingSource_CurrentChanged(object sender, EventArgs e)
        {

            var factura = (Factura)listadeFacturasBindingSource.Current;

            if (factura != null && factura.Id != 0 && factura.Activo == false)
            {
                label1.Visible = true;
            }
            else
            {
                label1.Visible = false;
            }
        }

    }
}
