using System;
using IdentityServer.SiteFinity.EntityFramework;
using IdentityServer.SiteFinity.EntityFramework.Services;
using IdentityServer.SiteFinity.Services;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.EntityFramework;

namespace IdentityServer.SiteFinity.Configuration
{
    public static class SiteFinityServiceFactoryExtensions
    {
        public static void RegisterSiteFinityConfigurationServices(this SiteFinityServiceFactory factory, EntityFrameworkServiceOptions efOptions)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (efOptions == null) throw new ArgumentNullException("efOptions");
            factory.Register(new Registration<SiteFinityConfigurationDbContext>(new SiteFinityConfigurationDbContext(efOptions.ConnectionString, efOptions.Schema)));
            factory.SiteFinityRelyingPartyService = new Registration<ISiteFinityRelyingPartyService>(typeof(SiteFinityRelyingPartyService));

        }
    }
}