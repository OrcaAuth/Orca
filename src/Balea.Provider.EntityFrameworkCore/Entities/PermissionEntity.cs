﻿using System.Collections.Generic;

namespace Balea.Provider.EntityFrameworkCore.Entities
{
	public class PermissionEntity
	{
		public PermissionEntity(string name, int applicationId)
		{
			Name = name;
			ApplicationId = applicationId;
		}

		public PermissionEntity(string name)
		{
			Name = name;
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int ApplicationId { get; set; }
		public ApplicationEntity Application { get; set; }
		public ICollection<RolePermissionEntity> Roles { get; set; }
		public ICollection<PermissionTagEntity> Tags { get; set; }
	}
}
