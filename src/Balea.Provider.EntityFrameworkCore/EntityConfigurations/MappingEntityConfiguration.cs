﻿using Balea.Provider.EntityFrameworkCore.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balea.Provider.EntityFrameworkCore.EntityConfigurations
{
	internal class MappingEntityConfiguration : IEntityTypeConfiguration<MappingEntity>
	{
		public void Configure(EntityTypeBuilder<MappingEntity> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name)
				.HasMaxLength(200)
				.IsRequired();
			builder
				.HasIndex(x => x.Name)
				.IsUnique();
		}
	}
}
