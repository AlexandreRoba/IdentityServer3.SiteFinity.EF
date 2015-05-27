using AutoMapper;

namespace IdentityServer.SiteFinity.EntityFramework.Entities
{
    public static class EntitiesMap
    {
        static EntitiesMap()
        {
            Mapper.CreateMap<Entities.SiteFinityRelyingParty, Models.SiteFinityRelyingParty>(MemberList.Destination);
        }

        public static Models.SiteFinityRelyingParty ToModel(this Entities.SiteFinityRelyingParty sitefinityRelyingParty)
        {
            if (sitefinityRelyingParty == null) return null;
            return Mapper.Map<Entities.SiteFinityRelyingParty, Models.SiteFinityRelyingParty>(sitefinityRelyingParty);
        }
    }
}