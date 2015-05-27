using System.Data.Entity;
using IdentityServer.SiteFinity.EntityFramework.Entities;

namespace IdentityServer3.SiteFinity.EntityFramework
{
    public class SiteFinityConfigurationDbContext : BaseDbContext
    {
        public SiteFinityConfigurationDbContext()
            : this(EfConstants.ConnectionName)
        {

        }

        public SiteFinityConfigurationDbContext(string connectionString) : base(connectionString)
        {
        }

        public SiteFinityConfigurationDbContext(string connectionString, string schema) : base(connectionString, schema)
        {
        }

        public DbSet<SiteFinityRelyingParty> SiteFinityRelyingParties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SiteFinityRelyingParty>()
                .ToTable(EfConstants.TableNames.SiteFinityRelyingParties, Schema);
            
        }
    }
}