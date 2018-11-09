using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hcm.Database;
using hcm.Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hcm.Controllers.Users
{
    [Route("api/users"), ApiController]
    public class UserController
    {
        private HcmContext _dbContext;

        public UserController(HcmContext context)
        {
            this._dbContext = context;
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


    }
}