using System;
using IdentityServer.SiteFinity.EntityFramework.Services;
using IdentityServer.SiteFinity.Services;
using Thinktecture.IdentityServer.Core.Configuration;
using Thinktecture.IdentityServer.EntityFramework;

namespace IdentityServer.SiteFinity.EntityFramework.Regsitrations
{
    public class SiteFinityConfigurationRelyingPartyRegistration : Registration<ISiteFinityRelyingPartyService, SiteFinityRelyingPartyService>
    {
       
            public SiteFinityConfigurationRelyingPartyRegistration()
                : this(new EntityFrameworkServiceOptions())
            {
            }

            public SiteFinityConfigurationRelyingPartyRegistration(EntityFrameworkServiceOptions options)
            {
                if (options == null) throw new ArgumentNullException("options");

                this.AdditionalRegistrations.Add(new Registration<SiteFinityConfigurationDbContext>(resolver => new SiteFinityConfigurationDbContext(options.ConnectionString, options.Schema)));
            }
        
    }
}