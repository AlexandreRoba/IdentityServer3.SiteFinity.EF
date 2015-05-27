using System;
using System.Data.Entity;

namespace IdentityServer3.SiteFinity.EntityFramework
{
    public class BaseDbContext : DbContext
    {
        public string Schema { get; protected set; }

        public BaseDbContext(string connectionString)
            : this(connectionString, null)
        {
        }

        public BaseDbContext(string connectionString, string schema)
            : base(connectionString)
        {
            Schema = schema;
        }
    }
}