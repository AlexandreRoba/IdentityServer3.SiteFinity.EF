using System;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.SiteFinity.Services;
using IdentityServer.SiteFinity.EntityFramework.Entities;

namespace IdentityServer.SiteFinity.EntityFramework.Services
{
    public class SiteFinityRelyingPartyService : ISiteFinityRelyingPartyService
    {

        SiteFinityConfigurationDbContext context;

        public SiteFinityRelyingPartyService(SiteFinityConfigurationDbContext ctx)
        {
            this.context = ctx;
        }

        public async Task<Models.SiteFinityRelyingParty> GetByRealmAsync(string realm)
        {
            var relyingParty = await context.SiteFinityRelyingParties.SingleOrDefaultAsync(rp => rp.Realm.Equals(realm,StringComparison.OrdinalIgnoreCase) && rp.Enabled);
            return relyingParty.ToModel();
        }
    }
}