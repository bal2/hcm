using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using hcm.Database;
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

        public MeController(HcmContext context, UserService userService)
        {
            this._dbContext = context;
            this._userService = userService;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> GetMeAsync()
        {
            var user = await _userService.GetUserAsync(GetUserId());

            return Ok(new MeResourceModel(user));
        }

        private long GetUserId()
        {
            return long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
