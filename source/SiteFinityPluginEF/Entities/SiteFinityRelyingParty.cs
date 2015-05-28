using System.ComponentModel.DataAnnotations;

namespace IdentityServer.SiteFinity.EntityFramework.Entities
{
    public class SiteFinityRelyingParty
    {
        [Key]
        public virtual int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public virtual string Name { get; set; }
        [Required]
        [StringLength(2000)]
        public virtual string Realm { get; set; }
        [StringLength(2000)]
        public virtual string ReplyUrl { get; set; }
        [Required]
        [StringLength(256)]
        public virtual string Key { get; set; }
        [Required]
        [StringLength(200)]
        public virtual string Domain { get; set; }
        public virtual bool Enabled { get; set; }

    }
}
