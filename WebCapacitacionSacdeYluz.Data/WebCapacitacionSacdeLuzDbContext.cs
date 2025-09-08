using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebCapacitacionSacdeYluz.Data.Models;

namespace WebCapacitacionSacdeYluz.Data
{
    public class WebCapacitacionSacdeLuzDbContext : DbContext
    {
        public WebCapacitacionSacdeLuzDbContext() { }
        public WebCapacitacionSacdeLuzDbContext(DbContextOptions<WebCapacitacionSacdeLuzDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<DwdMarca> DwdMarca { get; set; }
        public virtual DbSet<DwdCalzado> DwdCalzado { get; set; }
        public virtual DbSet<DwdProveedor> DwdProveedor { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:yluz-core-test.database.windows.net,1433;Initial Catalog=sql-db-capacitacionSACDELuz;Persist Security Info=False;User ID=yluz-core;Password=Password.123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
