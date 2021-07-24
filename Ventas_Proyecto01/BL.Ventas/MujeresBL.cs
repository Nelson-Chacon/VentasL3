using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Ventas
{
    public class MujeresBL
    {
        Contexto _contexto;
        public BindingList<Mujer> ListaProdMujeres { get; set; } //Propiedad Listade productos

        public MujeresBL()
        {
            _contexto = new Contexto();
            ListaProdMujeres = new BindingList<Mujer>();


        }

        public BindingList<Mujer> obtenerProductos()
        {
            _contexto.Mujeres.Load();
            ListaProdMujeres = _contexto.Mujeres.Local.ToBindingList();
            return ListaProdMujeres;
        }
        public void CancelarCambios()

        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;

                item.Reload();
            }
        }
        public ResultadoMujer GuardarProdMujeres(Mujer mujer)
        {
            var resultadoMuj = Validar(mujer);
            if (resultadoMuj.Exitoso == false)
            {
                return resultadoMuj;
            }
            _contexto.SaveChanges();
            resultadoMuj.Exitoso = true;
            return resultadoMuj;
        }

        public void AgregarProdMujeres()
        {
            var nuevoProdMujeres = new Mujer();
            ListaProdMujeres.Add(nuevoProdMujeres);//se agrega un nuevo producto a la lista
        }
        public bool EliminarProdMujeres(int id)
        {
            foreach (var prodMujer in ListaProdMujeres)//recorre la lista de los objetos
            {
                if (prodMujer.Id == id)
                {
                    ListaProdMujeres.Remove(prodMujer);
                    _contexto.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        private ResultadoMujer Validar(Mujer mujer)
        {
            var resultado = new ResultadoMujer();

            resultado.Exitoso = true;

            if (mujer== null)
            {
                resultado.Mensaje = "agregue un producto valido";
                resultado.Exitoso = false;
            }

            if (string.IsNullOrEmpty(mujer.Descripcion) == true)
            {
                resultado.Mensaje = "Ingrese una descripcion";
                resultado.Exitoso = false;
            }

            if (mujer.SeccionId == 0)
            {
                resultado.Mensaje = "Seleccione una sección";
                resultado.Exitoso = false;
            }
      
            if (mujer.Existencia < 0)
            {
                resultado.Mensaje = "La existencia debe ser mayor que cero";
                resultado.Exitoso = false;
            }
            if (mujer.Precio < 0)
            {
                resultado.Mensaje = "El precio debe ser mayor que cero";
                resultado.Exitoso = false;
            }
            return resultado;
        }
    }
    public class Mujer
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int SeccionId { get; set; }
        public Seccion Seccion { get; set; }
        public int Existencia { get; set; }
        public byte [] Foto { get; set; }
        public bool Activo { get; set; }

        public Mujer()
        {
            Activo = true;
        }
    }
    public class ResultadoMujer
    {

        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
    }
}
