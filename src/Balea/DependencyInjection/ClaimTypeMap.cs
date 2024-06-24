﻿using System.Security.Claims;

namespace Balea
{
    public class ClaimTypeMap
    {
        public string RoleClaimType { get; set; } = ClaimTypes.Role;
        public string NameClaimType { get; set; } = ClaimTypes.Name;

        public ICollection<string> AllowedSubjectClaimTypes { get; } = new HashSet<string>()
        {
            ClaimTypes.NameIdentifier,
            JwtClaimTypes.ClientId
        };

        public string PermissionClaimType { get; set; } = BaleaClaims.Permission;
    }
}
