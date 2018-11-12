using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hcm.Database;
using hcm.Database.Models;
using hcm.Exceptions;
using hcm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hcm.Controllers.Users
{
    [Route("api/users"), ApiController]
    public class UserController : Controller
    {
        private HcmContext _dbContext;
        private UserService _userService;

        public UserController(HcmContext context, UserService userService)
        {
            this._dbContext = context;
            this._userService = userService;
        }

        [HttpGet, Authorize]
        public async Task<List<UserResourceModel>> getUsersAsync()
        {
            return await _dbContext.Users.Select(u => new UserResourceModel(u)).ToListAsync();
        }

        [HttpPost, Authorize]
        public async Task<UserResourceModel> postUserAsync([FromBody] UserCreateModel newUser)
        {
            User u = new User()
            {
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Title = newUser.Title,
                Phone = newUser.Phone
            };

            var hasher = new PasswordHasher<User>();

            u.PasswordHash = hasher.HashPassword(u, newUser.Password);

            await _dbContext.Users.AddAsync(u);
            await _dbContext.SaveChangesAsync();

            return new UserResourceModel(u);
        }

        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetUserAsync(long id)
        {
            try
            {
                var user = await _userService.GetUserAsync(id);
                return Ok(new UserDetailedResourceModel(user));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutUserAsync(long id, [FromBody] UserUpdateResourceModel u)
        {
            try
            {
                var user = await _userService.GetUserAsync(id);

                user.Email = u.Email;
                user.FirstName = u.FirstName;
                user.LastName = u.LastName;
                user.Title = u.Title;
                user.Phone = u.Phone;
                user.IsPictureApproved = u.IsPictureApproved;

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                return Ok(new UserDetailedResourceModel(user));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


    }
}