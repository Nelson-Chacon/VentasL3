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

namespace Win.TiendaRopa
{
    public partial class FormLogin : Form
    {
        SeguridadBL _seguridad;

        public FormLogin()
        {
            InitializeComponent();

            _seguridad = new SeguridadBL();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
        
        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            string usuario;
            string contrasena;

            usuario = textBox1.Text;
            contrasena = textBox2.Text;

            buttonAceptar.Enabled = false;
            buttonAceptar.Text = "Verificando...";
            Application.DoEvents();

            var usuarioDB = _seguridad.Autorizar(usuario, contrasena);

            if (usuarioDB != null)
            {
                this.Close();
                Utilidades.NombreUsuario = usuarioDB.Nombre;
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrecta");
            }
            buttonAceptar.Enabled = true;
            buttonAceptar.Text = "Aceptar";
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==  Convert.ToChar(Keys.Enter) && !string.IsNullOrEmpty(textBox1.Text))
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && !string.IsNullOrEmpty(textBox2.Text))
            {
                buttonAceptar.PerformClick();
            }
        }
    }
}
