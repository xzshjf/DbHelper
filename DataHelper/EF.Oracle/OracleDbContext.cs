using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Configuration;

namespace EF.Oracle
{
    
    public class OracleDbContext : DbContext
    {
        static readonly string SchemaName = ConfigurationManager.AppSettings["SchemaName"].ToString();
  
        public OracleDbContext()
         : base("OraConnstr")
        {
            Database.SetInitializer<OracleDbContext>(null);

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(SchemaName);

            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(38, 18));
        }
    }
}

