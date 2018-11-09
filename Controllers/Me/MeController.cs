using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using hcm.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hcm.Controllers.Me
{
    [Route("api/[controller]"), ApiController]
    public class MeController : Controller
    {

        private HcmContext _dbContext;

        public MeController(HcmContext context)
        {
            this._dbContext = context;
        }

        [HttpGet, Authorize]
        public IActionResult GetMe()
        {
            var user = _dbContext.Users.Where(u => u.UserId == GetUserId()).FirstOrDefault();

            return Ok(new MeResourceModel(user));
        }

        private long GetUserId()
        {
            return long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
