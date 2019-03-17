using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using hcm.Controllers.Roles;
using hcm.Controllers.Users;
using hcm.Database;
using hcm.Exceptions;
using hcm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hcm.Controllers.Me
{
    [Route("api/[controller]"), ApiController]
    public class MeController : Controller
    {

        private HcmContext _dbContext;
        private UserService _userService;
        private RoleService _roleService;

        public MeController(HcmContext context, UserService userService, RoleService roleService)
        {
            this._dbContext = context;
            this._userService = userService;
            this._roleService = roleService;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetMeAsync()
        {
            var user = await _userService.GetUserAsync(GetUserId());

            return Ok(new MeResourceModel(user));
        }

        [HttpPost("picture"), Authorize]
        public async Task<IActionResult> PostPictureAsync([FromBody] UserPictureResourceModel pic)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(pic.base64Picture);

                var user = await _userService.SetPictureAsync(GetUserId(), bytes);

                return Ok(new UserPictureResourceModel()
                {
                    base64Picture = Convert.ToBase64String(user.Picture)
                });
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("permissions"), Authorize]
        public async Task<IActionResult> GetPermissionsAsync()
        {
            return Ok((await _roleService.GetUserPermissions(GetUserId())).Select(p => new PermissionResourceModel(p)).ToList());
        }

        private long GetUserId()
        {
            return long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
