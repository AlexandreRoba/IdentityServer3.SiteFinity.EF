using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.SiteFinity.Configuration;
using IdentityServer.SiteFinity.EntityFramework;
using IdentityServer.SiteFinity.Models;
using Owin;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.EntityFramework;

namespace SelfHost.Config
{
    class Plugin
    {
        public static Action<IAppBuilder, IdentityServerOptions> Configure(string connectionSTring)
        {
            return (IAppBuilder pluginApp, IdentityServerOptions options) =>
            {
                var siteFinityOptions = new SiteFinityPluginOptions(options);

                var efConfig = new EntityFrameworkServiceOptions
                {
                    ConnectionString = "IdSvr3Config",
                    //Schema = "foo"
                };

                siteFinityOptions.Factory.RegisterSiteFinityConfigurationServices(efConfig);

                //Prepopulate the SiteFinity Relying party list with the values
                ConfigureSiteFInityRelyingParties(SiteFinityRelyingParties.Get(), efConfig);

                pluginApp.UseSiteFinityPlugin(siteFinityOptions);
            };
        }


        private static void ConfigureSiteFInityRelyingParties(IEnumerable<SiteFinityRelyingParty> sitefinityRelyingPerties,
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

    }
}
