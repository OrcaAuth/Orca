﻿using Microsoft.EntityFrameworkCore;
using System;

namespace Balea.Grantor.EntityFrameworkCore.Options
{
    /// <summary>
    /// Provide programatically configuration for <see cref="BaleaDbContext"/>.
    /// </summary>
    public class StoreOptions
    {
        public Action<DbContextOptionsBuilder> ConfigureDbContext { get; set; }
    }
}
