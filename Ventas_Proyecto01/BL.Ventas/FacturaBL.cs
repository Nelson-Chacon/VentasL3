using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BL.Ventas
{
    public class FacturaBL
    {
        Contexto _contexto;

        public BindingList<Factura> ListadeFacturas { get; set; }

        public FacturaBL()
        {
            _contexto = new Contexto();

            
        }
        public BindingList<Factura> ObtenerFacturas()
        {
            _contexto.Facturas.Include("FacturaDetalle").Load();

            ListadeFacturas = _contexto.Facturas.Local.ToBindingList();

            return ListadeFacturas;
        }
        
        public void AgregarFactura()
        {
            var nuevaFactura = new Factura();
            ListadeFacturas.Add(nuevaFactura);
        }

        public void AgregarFacturaDetalle(Factura factura)
        {
            if(factura != null)
            {
                var nuevoDetalle = new FacturaDetalle();
                factura.FacturaDetalle.Add(nuevoDetalle);
            }
        }

        public void RemoverFacturaDetalle(Factura factura,FacturaDetalle facturaDetalle)
        {
            if (factura!= null && facturaDetalle !=null)
            {
                factura.FacturaDetalle.Remove(facturaDetalle);
            }
        }

        public void CancelarCambios()

        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;

                item.Reload();
            }
        }
        
        public ResultadoFactura GuardarFactura(Factura factura)
        {
            var resultadoFactura = Validar(factura);

            if (resultadoFactura.Exitoso == false)
            {
                return resultadoFactura;
            }


            CalcularExistenciaMujer(factura);
            
            _contexto.SaveChanges();

            resultadoFactura.Exitoso = true;

            return resultadoFactura;
        }

        private void CalcularExistenciaMujer(Factura factura)//calcula la exitencia de cada producto
        {
            foreach (var detalle in factura.FacturaDetalle)
            {
                var producto = _contexto.Mujeres.Find(detalle.MujerId);

                if (producto != null)

                {
                    if (factura.Activo == true)
                    {
                        producto.Existencia = producto.Existencia - detalle.Cantidad;
                    }
                    else
                    {
                        producto.Existencia = producto.Existencia + detalle.Cantidad;
                    }
                }
            }
        }
        
        private ResultadoFactura Validar(Factura factura)
        {
            var resultadoFactura = new ResultadoFactura();

            resultadoFactura.Exitoso = true;

            if (factura == null)
            {
                resultadoFactura.Mensaje = "Agregue una factura para poder guardarla";
                resultadoFactura.Exitoso = false;

                return resultadoFactura;
            }
            if (factura.Id != 0 && factura.Activo == true)
            {
                resultadoFactura.Mensaje = "La factura ya fue emitida y no se pueden realizar cambios en ella";
                resultadoFactura.Exitoso = false;
            }
            
            if (factura.Activo == false)
            {
                resultadoFactura.Mensaje = "La factura esta anulada y no se pueden realizar cambios en ella";
                resultadoFactura.Exitoso = false;
            }

            if (factura.ClienteId == 0)
            {
                resultadoFactura.Mensaje = "Seleccione un cliente";
                resultadoFactura.Exitoso = false;
            }

            if (factura.FacturaDetalle.Count == 0)
            {
                resultadoFactura.Mensaje = "Agregue productos a la factura";
                resultadoFactura.Exitoso = false;
            }

            foreach (var detalle in factura.FacturaDetalle)
            {
                if (detalle.MujerId == 0)
                {
                    resultadoFactura.Mensaje = "Seleccione productos validos";
                    resultadoFactura.Exitoso = false;
                }
            }

            return resultadoFactura;
        }

        public void CalcularFactura (Factura factura)

        {
            if (factura !=null)
            {
                double subtotal = 0;

                foreach (var detalle in factura.FacturaDetalle)
                {
                    var producto = _contexto.Mujeres.Find(detalle.MujerId);
                    if (producto != null)
                    {
                        detalle.Precio = producto.Precio;
                        detalle.Total = detalle.Cantidad * producto.Precio;

                        subtotal += detalle.Total;
                    }
                }

                factura.Subtotal = subtotal;
                factura.Impuesto = subtotal * 0.15;
                factura.Total = subtotal + factura.Impuesto;
            }
        }

        public bool AnularFactura (int id)
        {
            foreach (var factura in ListadeFacturas)
            {
                if (factura.Id == id)
                {
                    factura.Activo = false;

                    CalcularExistenciaMujer(factura);

                    _contexto.SaveChanges();

                    return true;
                }
            }

            return false;
        }

    }

    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public Clientes Cliente{ get; set; }
        public BindingList<FacturaDetalle> FacturaDetalle { get; set; }
        public double Subtotal { get; set; }
        public double Impuesto { get; set; }
        public double  Total { get; set; }
        public bool Activo { get; set; }

        public Factura()
        {
            Fecha = DateTime.Now;
            FacturaDetalle = new BindingList<FacturaDetalle>();
            Activo = true;
        }
    }
    public class FacturaDetalle
    {
        public int Id { get; set; }
        public int MujerId { get; set; }
        public Mujer Mujer { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Total { get; set; }

        public FacturaDetalle()
        {
            Cantidad = 1;
        }
    }

    public class ResultadoFactura:Resultado
    {

    }

}
