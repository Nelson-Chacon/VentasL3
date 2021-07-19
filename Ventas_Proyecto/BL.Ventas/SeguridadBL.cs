using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Ventas
{
    public class SeguridadBL
    {

        Contexto _contexto;

        public SeguridadBL()
        {
            _contexto = new Contexto();
        }
        public bool Autorizar(string usuario, string contraseña)
        {

            var usuarios = _contexto.usuarios.ToList();

            foreach (var usuarioDb in usuarios)
            {


                if (usuario == usuarioDb.Nombre && contraseña == usuarioDb.Contraseña)
                {
                    return true;
                }

            }
            return false;
        }
    }
    public class Usuarios
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
    }
    
}
