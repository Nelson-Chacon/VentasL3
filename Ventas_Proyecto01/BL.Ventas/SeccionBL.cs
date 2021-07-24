using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Ventas
{
    public class SeccionBL
    {

        Contexto _contexto;
        public BindingList<Seccion> ListaSeccion { get; set; }

        public SeccionBL()
        {
            _contexto = new Contexto();
            ListaSeccion = new BindingList<Seccion>();
        }
        public BindingList<Seccion> ObtenerSeccion()

        {
            _contexto.SeccionRopa.Load();
            ListaSeccion = _contexto.SeccionRopa.Local.ToBindingList();

            return ListaSeccion;
        }
    }
    public class Seccion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
