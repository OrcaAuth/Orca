﻿using Microsoft.EntityFrameworkCore;

using Balea.Store.EntityFrameworkCore.Entities;

namespace Balea.Store.EntityFrameworkCore;

public class PermissionStore : IPermissionStore
{
    private readonly PermissionMapper _mapper = new();

    private readonly BaleaDbContext _context;

    public PermissionStore(BaleaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Permission> FindByIdAsync(string permissionId, CancellationToken cancellationToken)
    {
        var entity = await _context.Permissions.FindAsync(permissionId, cancellationToken);
        return _mapper.FromEntity(entity);
    }

    public async Task<Permission> FindByNameAsync(string permissionName, CancellationToken cancellationToken)
    {
        var entity = await _context.Permissions.FindByNameAsync(permissionName, cancellationToken);
        return _mapper.FromEntity(entity);
    }

    public async Task<AccessControlResult> CreateAsync(Permission permission, CancellationToken cancellationToken)
    {
        var entity = _mapper.ToEntity(permission);
        entity.Id = Guid.NewGuid().ToString();

        await _context.Permissions.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        _mapper.FromEntity(entity, permission);

        return AccessControlResult.Success;
    }

    public async Task<AccessControlResult> UpdateAsync(Permission permission, CancellationToken cancellationToken)
    {
        var entity = await _context.Permissions.FindAsync(permission.Id, cancellationToken);

        if (entity is null)
        {
            return AccessControlResult.Failed(new AccessControlError { Description = "Not found." });
        }

        _mapper.ToEntity(permission, entity);

        _context.Permissions.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        _mapper.FromEntity(entity, permission);

        return AccessControlResult.Success;
    }

    public async Task<AccessControlResult> DeleteAsync(Permission permission, CancellationToken cancellationToken)
    {
        var entity = await _context.Permissions.FindAsync(permission.Id, cancellationToken);

        if (entity is null)
        {
            return AccessControlResult.Failed(new AccessControlError { Description = "Not found." });
        }

        _context.Permissions.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return AccessControlResult.Success;
    }

    public async Task<IList<string>> GetRolesAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Permissions.FindAsync(permission.Id, cancellationToken);

        var targets = _context.RolePermissions.Where(x => x.PermissionId == entity.Id);
        var roles = targets.Select(x => x.Role.Name).ToList();

        return roles;
    }

    public async Task<AccessControlResult> AddRoleAsync(Permission permission, string roleName, CancellationToken cancellationToken)
    {
        var entity = await _context.Permissions.FindAsync(permission.Id, cancellationToken);
        var target = await _context.Roles.FindByNameAsync(roleName, cancellationToken);

        var binding = new RolePermissionEntity
        {
            RoleId = target.Id,
            PermissionId = entity.Id,
        };

        await _context.RolePermissions.AddAsync(binding, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return AccessControlResult.Success;
    }

    public async Task<AccessControlResult> RemoveRoleAsync(Permission permission, string roleName, CancellationToken cancellationToken)
    {
        var binding = await _context.RolePermissions
            .Where(x => x.Permission.Id == permission.Id)
            .Where(x => x.Role.Name == roleName)
            .FirstOrDefaultAsync(cancellationToken);

        if (binding is null)
        {
            return AccessControlResult.Failed(new AccessControlError { Description = "Not found." });
        }

        _context.RolePermissions.Remove(binding);

        await _context.SaveChangesAsync(cancellationToken);

        return AccessControlResult.Success;
    }

    public Task<IList<Permission>> ListAsync(CancellationToken cancellationToken = default)
    {
        var entities = _context.Permissions;

        var result = entities.Select(_mapper.FromEntity).ToList();

        return Task.FromResult<IList<Permission>>(result);
    }

    public Task<IList<Permission>> SearchAsync(PermissionFilter filter, CancellationToken cancellationToken = default)
    {
        var source = _context.Permissions.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
        {
            var words = filter.Name.Split().Where(word => word != string.Empty);
            source = source.Where(permission => words.All(word => permission.Name.Contains(word)));
        }

        if (!string.IsNullOrEmpty(filter.Description))
        {
            var words = filter.Description.Split().Where(word => word != string.Empty);
            source = source.Where(permission => words.All(word => permission.Description.Contains(word)));
        }

        if (filter.Roles is not null)
        {
            var bindings = _context.RolePermissions.Where(x => filter.Roles.Contains(x.Role.Name));

            source = source.Join(
                bindings,
                permission => permission.Id,
                binding => binding.PermissionId,
                (permission, binding) => permission
            );
        }

        var permissions = source.Select(_mapper.FromEntity).ToList();

        return Task.FromResult<IList<Permission>>(permissions);
    }
}
