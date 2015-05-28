using System.Collections.Generic;
using IdentityServer.SiteFinity.Models;

namespace SelfHost.Config
{
    public static class SiteFinityRelyingParties
    {
        public static IEnumerable<SiteFinityRelyingParty> Get()
        {
            return new List<SiteFinityRelyingParty>
            {
                new SiteFinityRelyingParty()
                {
                    Name = "SitefinityWebApp",
                    Domain = "Default",
                    Enabled = true,
                    Key = "KJGHJGFYTUGUYGUYF1111456454654gfd65h4d65g4h65dfg4h654dhg654fdg65h4dfg65h4d6f5h4g65dfg",
                    Realm = "http://localhost:44301/",
                    ReplyUrl = "http://localhost:44301"
                }
            };
        }
    }
}