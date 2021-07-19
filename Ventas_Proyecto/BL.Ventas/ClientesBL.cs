using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Ventas
{
    public class ClientesBL
    {
        Contexto _contexto;
        public BindingList<Clientes> ListaClientes { get; set; }

        public ClientesBL()
        {
            _contexto = new Contexto();

            ListaClientes =  new BindingList<Clientes>();
        }
        public BindingList<Clientes> ObtenerClientes()
        {
            _contexto.Clientes.Load();

            ListaClientes = _contexto.Clientes.Local.ToBindingList();

            return ListaClientes;
        }

        public void CancelarCambios()

        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;

                item.Reload();
            }
        }


        public ResultadoClientes GuardarClientes(Clientes cliente)
        {
            var resultadoCliente = Validar(cliente);

            if (resultadoCliente.Exitoso == false)
            {
                return resultadoCliente;
            }
            _contexto.SaveChanges();
            resultadoCliente.Exitoso = true;
            return resultadoCliente;
        }
        public void AgregarCliente()
        {
            var nuevoCliente = new Clientes();
            ListaClientes.Add(nuevoCliente);
        }
        public bool EliminarClientes(int id)
        {
            foreach (var clientes in ListaClientes)//recorre la lista de los objetos
            {
                if (clientes.Id == id)
                {
                    ListaClientes.Remove(clientes);
                    return true;
                }

            }
            return false;
        }
        private ResultadoClientes Validar(Clientes cliente)
        {
            var resultadoClientes = new ResultadoClientes();
            resultadoClientes.Exitoso = true;

            if (cliente == null)
            {
                resultadoClientes.Mensaje = "Agregue un cliente valido";
                resultadoClientes.Exitoso = false;

                return resultadoClientes;
            }

            if (string.IsNullOrEmpty(cliente.Nombre) == true)
            {
                resultadoClientes.Mensaje = "Ingrese el nombre del cliente";
                resultadoClientes.Exitoso = false;
            }

            return resultadoClientes;
        
    }


    }
    public class Clientes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
    }
    public class ResultadoClientes
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        
    }
}
