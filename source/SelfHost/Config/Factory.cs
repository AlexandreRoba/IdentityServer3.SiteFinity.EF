using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer.SiteFinity.Configuration;
using IdentityServer.SiteFinity.EntityFramework;
using IdentityServer.SiteFinity.EntityFramework.Services;
using IdentityServer.SiteFinity.Models;
using Owin;
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
            

            var factory = new IdentityServerServiceFactory();

            factory.RegisterConfigurationServices(efConfig);
            factory.RegisterOperationalServices(efConfig);

            factory.CorsPolicyService = new ClientConfigurationCorsPolicyRegistration(efConfig);

            var userService = new Thinktecture.IdentityServer.Core.Services.InMemory.InMemoryUserService(Users.Get());
            factory.UserService = new Registration<IUserService>(resolver => userService);

            ////Prepopulate the SiteFinity Relying party list with the values
            //ConfigureSiteFInityRelyingParties(SiteFinityRelyingParties.Get(), efConfig);
            ////Configure the SiteFinity relying Parties
            //factory.RegisterSiteFinityConfigurationServices(efConfig);
            
            return factory;
        }
        
        private static void ConfigureClients(IEnumerable<Client> clients, EntityFrameworkServiceOptions options)
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

        private static void ConfigureScopes(IEnumerable<Scope> scopes, EntityFrameworkServiceOptions options)
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