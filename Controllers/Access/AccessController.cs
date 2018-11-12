using System.Linq;
using System.Threading.Tasks;
using hcm.Controllers.Users;
using hcm.Database;
using hcm.Database.Models;
using hcm.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hcm.Controllers.Access
{
    [Route("api/[controller]"), ApiController]
    public class AccessController : Controller
    {
        private HcmContext _dbContext;
        private UserService _userService;

        public AccessController(HcmContext context, UserService userService)
        {
            this._dbContext = context;
            this._userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> PostAccessAsync([FromBody] AccessPostResourceController a)
        {
            var user = await _dbContext.Users.Where(u => u.CardId == a.CardId).FirstOrDefaultAsync();

            if (user == null)
                return NotFound("Card id not found");

            var c = new CardAccess()
            {
                CardId = user.CardId,
                UserId = user.UserId
            };

            await _dbContext.CardAccesses.AddAsync(c);
            await _dbContext.SaveChangesAsync();

            return Ok(new UserResourceModel(user));
        }

    }
}