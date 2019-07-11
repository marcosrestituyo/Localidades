using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Localidades.Models
{
    public class LocalidadesContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public LocalidadesContext() : base("name=LocalidadesContext")
        {
        }

        public System.Data.Entity.DbSet<Localidades.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<Localidades.Models.Region> Regions { get; set; }

        public System.Data.Entity.DbSet<Localidades.Models.Provincia> Provincias { get; set; }

        public System.Data.Entity.DbSet<Localidades.Models.Municipio> Municipios { get; set; }

        public System.Data.Entity.DbSet<Localidades.Models.DistritoMunicipal> DistritoMunicipals { get; set; }

        public System.Data.Entity.DbSet<Localidades.Models.Seccion> Seccions { get; set; }

        public System.Data.Entity.DbSet<Localidades.Models.Sector> Sectors { get; set; }

        public System.Data.Entity.DbSet<Localidades.Models.Calle> Calles { get; set; }

        public System.Data.Entity.DbSet<Localidades.Models.Local> Locals { get; set; }
    }
}
