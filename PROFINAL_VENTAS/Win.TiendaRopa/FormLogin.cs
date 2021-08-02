﻿using BL.Ventas;
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

            var resultado = _seguridad.Autorizar(usuario, contrasena);

            if (resultado == true)
            {
                this.Close();

            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrecta");
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}