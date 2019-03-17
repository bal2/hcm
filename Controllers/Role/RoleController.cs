using System.Linq;
using System.Threading.Tasks;
using hcm.DTOs.Roles;
using hcm.Exceptions;
using hcm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hcm.Controllers.Roles
{
    [Route("api/roles"), ApiController]
    public class RoleController : Controller
    {
        private RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        //Get all permissions
        [HttpGet("permissions")]
        public async Task<IActionResult> GetAllPermissionsAsync()
        {
            return Ok((await _roleService.GetAllPermissionsAsync()).Select(x => new PermissionResourceModel(x)).ToList());
        }

        [HttpGet(Name = "GetRoles"), Authorize("ViewRoles")]
        public IActionResult GetRoles([FromQuery] ListQueryArgs args)
        {
            var r = _roleService.GetRoles(args);

            return Ok(new ListResModel<RoleResourceModel>
            {
                Paging = ListResHelper.GenerateHeaderFromPagedList(r),
                Links = ListResHelper.GenerateLinksFromPagedList(Url, "GetRoles", r),
                Items = r.List.Select(x => new RoleResourceModel(x)).ToList()
            });
        }

        [HttpGet("{id}"), Authorize("ViewRoles")]
        public async Task<IActionResult> GetRolesAsync(long id)
        {
            try
            {
                var r = await _roleService.GetRoleAsync(id);

                return Ok(r);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost, Authorize("CreateRole")]
        public async Task<IActionResult> PostRoleAsync([FromBody] RoleCreateResourceModel n)
        {
            try
            {
                CreateUpdateRoleDTO nr = new CreateUpdateRoleDTO()
                {
                    Name = n.Name,
                    Description = n.Description
                };

                var r = await _roleService.CreateRoleAsync(nr);

                return Ok(r);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
        }

        [HttpPut("{id}"), Authorize("UpdateRole")]
        public async Task<IActionResult> UpdateRoleAsync(long id, [FromBody] RoleUpdateResourceModel u)
        {
            try
            {
                CreateUpdateRoleDTO ur = new CreateUpdateRoleDTO()
                {
                    Name = u.Name,
                    Description = u.Description
                };

                var r = await _roleService.UpdateRoleAsync(id, ur);

                return Ok(r);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}"), Authorize("DeleteRole")]
        public async Task<IActionResult> DeleteRolAsync(long id)
        {
            try
            {
                await _roleService.DeleteRoleAsync(id);

                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        //TODO: Paging?
        [HttpGet("{id}/users"), Authorize("ViewRoles")]
        public async Task<IActionResult> GetUsersAsync(long id)
        {
            try
            {
                var u = await _roleService.GetRoleUsersAsync(id);

                return Ok(u.Select(x => new RoleUserResourceModel(x)).ToList());
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }

        }

        [HttpPost("{id}/users"), Authorize("UpdateRole")]
        public async Task<IActionResult> PostUserAsync(long id, [FromBody] RoleUserAddResourceModel nu)
        {
            try
            {
                await _roleService.AddUserToRoleAsync(id, nu.UserId);

                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}/users/{userId}"), Authorize("UpdateRole")]
        public async Task<IActionResult> DeleteUserAsync(long id, long userId)
        {
            try
            {
                await _roleService.RemoveUserFromRoleAsync(id, userId);

                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/permissions"), Authorize("ViewRoles")]
        public async Task<IActionResult> GetRolePermissionsAsync(long id)
        {
            try
            {
                return Ok((await _roleService.GetRolePermissionsAsync(id)).Select(p => new PermissionResourceModel(p)).ToList());
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{id}/permissions"), Authorize("UpdateRole")]
        public async Task<IActionResult> PostRolePermissionAsync(long id, [FromBody] PermissionAddResourceModel p)
        {
            try
            {
                await _roleService.AddPermissionToRoleAsync(id, p.PermissionId);

                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}/permissions/{permissionId}"), Authorize("UpdateRole")]
        public async Task<IActionResult> DeleteRolePermissionAsync(long id, long permissionId)
        {
            try
            {
                await _roleService.RemovePermissionFromRoleAsync(id, permissionId);

                return Ok();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}