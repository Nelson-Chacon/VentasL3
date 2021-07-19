using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Ventas
{
    public class DatosdeInicio: CreateDatabaseIfNotExists<Contexto>
    {
        protected override void Seed(Contexto contexto)
        {

            var usuarioAdmin = new Usuarios();
            usuarioAdmin.Nombre = "admin";
            usuarioAdmin.Contraseña = "tienda";
            contexto.usuarios.Add(usuarioAdmin);


            //Datos de seccion de departamentos
            var seccion1 = new Seccion();
            seccion1.Descripcion = "Camisas Formales";
            contexto.SeccionRopa.Add(seccion1);

            var seccion2 = new Seccion();
            seccion2.Descripcion = "Camisa Polo";
            contexto.SeccionRopa.Add(seccion2);

            var seccion3 = new Seccion();
            seccion3.Descripcion = "Camisetas";
            contexto.SeccionRopa.Add(seccion3);

            var seccion4 = new Seccion();
            seccion4.Descripcion = "Jeans";
            contexto.SeccionRopa.Add(seccion4);

            var seccion5 = new Seccion();
            seccion5.Descripcion = "Pantalon Formal";
            contexto.SeccionRopa.Add(seccion5);

            var seccion6 = new Seccion();
            seccion6.Descripcion = "Ropa Deportiva";
            contexto.SeccionRopa.Add(seccion6);

            var seccion7 = new Seccion();
            seccion7.Descripcion = "Ropa Interior";
            contexto.SeccionRopa.Add(seccion7);
            
            var seccion8 = new Seccion();
            seccion8.Descripcion = "Calzado Formal";
            contexto.SeccionRopa.Add(seccion8);

            var seccion9 = new Seccion();
            seccion9.Descripcion = "Calzado Deportivo";
            contexto.SeccionRopa.Add(seccion9);

            var seccion10 = new Seccion();
            seccion10.Descripcion = "Joyeria";
            contexto.SeccionRopa.Add(seccion10);


            base.Seed(contexto);
        }
    }
}
