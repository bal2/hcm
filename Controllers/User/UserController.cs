using System;
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

        [HttpGet(Name = "GetUsers"), Authorize("ViewUsers")]
        public IActionResult getUsers([FromQuery] ListQueryArgs args)
        {
            var m = _userService.GetUsers(args);

            return Ok(new ListResModel<UserResourceModel>
            {
                Paging = ListResHelper.GenerateHeaderFromPagedList(m),
                Links = ListResHelper.GenerateLinksFromPagedList(Url, "GetUsers", m),
                Items = m.List.Select(x => new UserResourceModel(x)).ToList()
            });
        }

        [HttpPost, Authorize("CreateUser")]
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

        [HttpGet("{id}"), Authorize("ViewUsers")]
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

        [HttpPut("{id}"), Authorize("UpdateUser")]
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
                user.CardId = u.CardId;

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();

                return Ok(new UserDetailedResourceModel(user));
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{id}/picture"), Authorize("UpdateUser")]
        public async Task<IActionResult> PostUserPictureAsync(long id, UserPictureResourceModel pic)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(pic.base64Picture);

                var user = await _userService.SetPictureAsync(id, bytes);

                return Ok(new UserPictureResourceModel()
                {
                    base64Picture = Convert.ToBase64String(user.Picture)
                });
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("zip")]
        public IActionResult DownloadUsersZipFile()
        {
            var p = _userService.GenerateUsersZipFile();

            byte[] fileBytes = System.IO.File.ReadAllBytes(p);

            return File(fileBytes, "application/force-download", "users.zip");
        }
    }
}