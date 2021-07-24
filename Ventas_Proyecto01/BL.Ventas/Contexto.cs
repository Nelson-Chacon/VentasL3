using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL.Ventas
{
    public class Contexto: DbContext
    {
        public Contexto():base("TIENDADEROPA")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            Database.SetInitializer(new DatosdeInicio());
        }
        public DbSet<Usuarios> usuarios { get; set; }

        public DbSet<Niños> Niño { get; set; }

        public DbSet<Hombre> Hombres { get; set; }

        public DbSet<Mujer> Mujeres { get; set; }

        public DbSet<Seccion> SeccionRopa { get; set; }

        public DbSet<Clientes> Clientes { get; set; }

        public DbSet<Factura> Facturas { get; set; }



    }
}
