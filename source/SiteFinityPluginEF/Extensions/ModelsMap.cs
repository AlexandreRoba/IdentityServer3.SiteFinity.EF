using AutoMapper;

namespace IdentityServer.SiteFinity.Models
{
    public static class EntitiesMap
    {
        static EntitiesMap()
        {
            Mapper.CreateMap<SiteFinityRelyingParty, EntityFramework.Entities.SiteFinityRelyingParty>(MemberList.Source);
        }

        public static EntityFramework.Entities.SiteFinityRelyingParty ToEntity(this SiteFinityRelyingParty siteFinityRelyingParty)
        {
            if (siteFinityRelyingParty == null) return null;


            return Mapper.Map<SiteFinityRelyingParty, EntityFramework.Entities.SiteFinityRelyingParty>(siteFinityRelyingParty);
        }
    }
}
