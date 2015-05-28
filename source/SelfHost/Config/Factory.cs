using System.Collections.Generic;
using System.Linq;
using IdentityServer.SiteFinity.EntityFramework;
using IdentityServer.SiteFinity.Models;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.Core.Models;
using Thinktecture.IdentityServer.Core.Resources;
using Thinktecture.IdentityServer.Core.Services;
using Thinktecture.IdentityServer.EntityFramework;

namespace SelfHost.Config
{
    public class Factory
    {
        public static IdentityServerServiceFactory Configure(string connString)
        {
            var efConfig = new EntityFrameworkServiceOptions
            {
                ConnectionString = connString,
                //Schema = "foo"
            };

            var cleanup = new TokenCleanup(efConfig, 10);
            cleanup.Start();

            // these two calls just pre-populate the test DB from the in-memory config
            ConfigureClients(Clients.Get(), efConfig);
            ConfigureScopes(Scopes.Get(), efConfig);
            ConfigureSiteFInityRelyingParties(SiteFinityRelyingParties.Get(), efConfig);

            var factory = new IdentityServerServiceFactory();

            factory.RegisterConfigurationServices(efConfig);
            factory.RegisterOperationalServices(efConfig);

            factory.CorsPolicyService = new ClientConfigurationCorsPolicyRegistration(efConfig);

            var userService = new Thinktecture.IdentityServer.Core.Services.InMemory.InMemoryUserService(Users.Get());
            factory.UserService = new Registration<IUserService>(resolver => userService);

            return factory;
        }

        public static void ConfigureSiteFInityRelyingParties(IEnumerable<SiteFinityRelyingParty> sitefinityRelyingPerties,
            EntityFrameworkServiceOptions options)
        {
            using (var db = new SiteFinityConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (!db.SiteFinityRelyingParties.Any())
                {
                    foreach (var sfrp in sitefinityRelyingPerties)
                    {
                        var e = sfrp.ToEntity();
                        db.SiteFinityRelyingParties.Add(e);
                    }
                    db.SaveChanges();
                }
            }

        }

        public static void ConfigureClients(IEnumerable<Client> clients, EntityFrameworkServiceOptions options)
        {
            using (var db = new ClientConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (!db.Clients.Any())
                {
                    foreach (var c in clients)
                    {
                        var e = c.ToEntity();
                        db.Clients.Add(e);
                    }
                    db.SaveChanges();
                }
            }
        }

        public static void ConfigureScopes(IEnumerable<Scope> scopes, EntityFrameworkServiceOptions options)
        {
            using (var db = new ScopeConfigurationDbContext(options.ConnectionString, options.Schema))
            {
                if (!db.Scopes.Any())
                {
                    foreach (var s in scopes)
                    {
                        var e = s.ToEntity();
                        db.Scopes.Add(e);
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}