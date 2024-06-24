﻿using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.EntityFrameworkCore.Store.Infrastructure
{
    public class AuthorizeRoles : AuthorizeAttribute
    {
        public AuthorizeRoles(params string [] roles)
        {
            Roles = String.Join(",", roles);
        }
    }
}
