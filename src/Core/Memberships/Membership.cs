using Core.Common;
using Core.Locations;
using Core.Organizations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Memberships
{
    public class Membership : AuditableEntity
    {
        public bool IsOwner { get; set; }
        public bool IsMember { get; set; }
        public string UserId { get; set; }
        public int OrganizationId { get; set; }
        public int LocationId { get; set; }
        [JsonIgnore]
        public Organization Organization { get; set; }
        [JsonIgnore]
        public Location Location { get; set; }

    }
}
