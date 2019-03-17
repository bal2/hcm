using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hcm.Controllers;
using hcm.Database;
using hcm.Database.Models;
using hcm.DTOs.Groups;
using hcm.DTOs.Roles;
using hcm.DTOs.Users;
using hcm.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace hcm.Services
{
    public class RoleService
    {
        private HcmContext _dbContext;
        private UserService _userService;

        public RoleService(HcmContext dbContext, UserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public PagedList<RoleDTO> GetRoles(ListQueryArgs args)
        {
            var query = _dbContext.Roles.Select(r => new RoleDTO(r));
            return new PagedList<RoleDTO>(query, args.PageNumber, args.PageSize);
        }

        public async Task<RoleDTO> GetRoleAsync(long id)
        {
            return new RoleDTO(await GetRoleModelAsync(id));
        }

        public async Task<RoleDTO> CreateRoleAsync(CreateUpdateRoleDTO n)
        {
            if (await CheckIfRoleNameExistsAsync(n.Name))
                throw new BadRequestException("Role name already exists");

            Role r = new Role()
            {
                Name = n.Name,
                Description = n.Description
            };

            await _dbContext.Roles.AddAsync(r);
            await _dbContext.SaveChangesAsync();

            return new RoleDTO(r);
        }

        public async Task<RoleDTO> UpdateRoleAsync(long id, CreateUpdateRoleDTO u)
        {
            var r = await GetRoleModelAsync(id);

            if (await CheckIfRoleNameExistsAsync(u.Name, id))
                throw new BadRequestException("Role name already exists");

            r.Name = u.Name;
            r.Description = u.Description;

            _dbContext.Roles.Update(r);
            await _dbContext.SaveChangesAsync();

            return new RoleDTO(r);
        }

        public async Task DeleteRoleAsync(long id)
        {
            var r = await GetRoleModelAsync(id);

            _dbContext.Roles.Remove(r);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<PermissionDTO>> GetAllPermissionsAsync()
        {
            return await _dbContext.Permissions.Select(p => new PermissionDTO(p)).ToListAsync();
        }

        public async Task<List<PermissionDTO>> GetRolePermissionsAsync(long id)
        {
            var r = await GetRoleAsync(id);

            var q = from rolePermissions in _dbContext.RolePermissions
                    join permissions in _dbContext.Permissions on rolePermissions.PermissionId equals permissions.PermissionId
                    where rolePermissions.RoleId == r.RoleId
                    select new PermissionDTO(permissions);

            return await q.ToListAsync();
        }

        public async Task AddPermissionToRoleAsync(long roleId, long permissionId)
        {
            if (await CheckIfRoleContainsPermission(roleId, permissionId))
                throw new BadRequestException("Role already contains permission");

            var rp = new RolePermission()
            {
                RoleId = roleId,
                PermissionId = permissionId
            };

            await _dbContext.RolePermissions.AddAsync(rp);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemovePermissionFromRoleAsync(long roleId, long permissionId)
        {
            if (!await CheckIfRoleContainsPermission(roleId, permissionId))
                throw new BadRequestException("Role does not contain permission");

            var rp = await _dbContext.RolePermissions.Where(ro => ro.RoleId == roleId && ro.PermissionId == permissionId).FirstOrDefaultAsync();

            _dbContext.RolePermissions.Remove(rp);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUserToRoleAsync(long roleId, long userId)
        {
            if (await CheckIfRoleContainsUser(roleId, userId))
                throw new BadRequestException("Role already contains user");

            var ru = new RoleUser()
            {
                RoleId = roleId,
                UserId = userId
            };

            await _dbContext.RoleUsers.AddAsync(ru);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveUserFromRoleAsync(long roleId, long userId)
        {
            if (!await CheckIfRoleContainsUser(roleId, userId))
                throw new BadRequestException("Role does not contain user");

            var ru = await _dbContext.RoleUsers.Where(o => o.RoleId == roleId && o.UserId == userId).FirstOrDefaultAsync();

            _dbContext.RoleUsers.Remove(ru);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserDTO>> GetRoleUsersAsync(long roleId)
        {
            var r = await GetRoleModelAsync(roleId);

            var q = from roleUsers in _dbContext.RoleUsers
                    join users in _dbContext.Users on roleUsers.UserId equals users.UserId
                    where roleUsers.RoleId == roleId
                    select new UserDTO(users);

            return await q.ToListAsync();
        }

        public async Task<List<RoleDTO>> GetUserRoles(long userId)
        {
            var u = _userService.GetUserAsync(userId);

            var q = from roleUsers in _dbContext.RoleUsers
                    join roles in _dbContext.Roles on roleUsers.RoleId equals roles.RoleId
                    where roleUsers.UserId == userId
                    select new RoleDTO(roles);

            return await q.ToListAsync();
        }

        public async Task<List<PermissionDTO>> GetUserPermissions(long userId)
        {
            var u = _userService.GetUserAsync(userId);

            var q = from roleUsers in _dbContext.RoleUsers
                    join roles in _dbContext.Roles on roleUsers.RoleId equals roles.RoleId
                    join rolePermissions in _dbContext.RolePermissions on roles.RoleId equals rolePermissions.RoleId
                    join permissions in _dbContext.Permissions on rolePermissions.PermissionId equals permissions.PermissionId
                    where roleUsers.UserId == userId
                    select new PermissionDTO(permissions);

            return await q.ToListAsync();
        }

        private async Task<bool> CheckIfRoleNameExistsAsync(string name, long? ignoreId = null)
        {
            if (ignoreId == null)
                return (await _dbContext.Roles.Where(r => r.Name == name).FirstOrDefaultAsync()) != null;
            else
                return (await _dbContext.Roles.Where(r => r.Name == name && r.RoleId != ignoreId).FirstOrDefaultAsync() != null);
        }

        private async Task<bool> CheckIfRoleContainsPermission(long roleId, long permissionId)
        {
            var r = await GetRoleModelAsync(roleId);
            var p = await GetPermissionModelAsync(permissionId);

            return await _dbContext.RolePermissions.Where(rp => rp.RoleId == roleId && rp.PermissionId == permissionId).FirstOrDefaultAsync() != null;
        }

        private async Task<bool> CheckIfRoleContainsUser(long roleId, long userId)
        {
            var r = await GetRoleModelAsync(roleId);
            var u = await _userService.GetUserAsync(userId);

            return await _dbContext.RoleUsers.Where(ru => ru.RoleId == roleId && ru.UserId == ru.UserId).FirstOrDefaultAsync() != null;
        }

        private async Task<Permission> GetPermissionModelAsync(long permissionId)
        {
            var p = await _dbContext.Permissions.FindAsync(permissionId);

            if (p == null)
                throw new NotFoundException("Permission not found");

            return p;
        }

        private async Task<Role> GetRoleModelAsync(long id)
        {
            var r = await _dbContext.Roles.FindAsync(id);

            if (r == null)
                throw new NotFoundException("Role not found");

            return r;
        }
    }
}