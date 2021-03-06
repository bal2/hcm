using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using hcm.DTOs.Groups;
using hcm.Exceptions;
using hcm.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hcm.Controllers.Groups
{
    [Route("api/groups"), ApiController]
    public class GroupController : Controller
    {
        private GroupService _groupService;

        public GroupController(GroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet(Name = "GetGroups"), Authorize("ViewGroups")]
        public IActionResult GetGroups([FromQuery] ListQueryArgs args)
        {
            var m = _groupService.GetGroups(args);

            return Ok(new ListResModel<GroupDTO>
            {
                Paging = ListResHelper.GenerateHeaderFromPagedList(m),
                Links = ListResHelper.GenerateLinksFromPagedList(Url, "GetGroups", m),
                Items = m.List
            });
        }

        [HttpGet("{id}"), Authorize("ViewGroups")]
        public async Task<IActionResult> GetGroupAsync(long id)
        {
            try
            {
                var g = await _groupService.GetGroupAsync(id);

                return Ok(g);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost, Authorize("CreateGroup")]
        public async Task<IActionResult> PostGroupAsync([FromBody] GroupCreateResourceModel n)
        {
            try
            {


                NewGroupDTO ng = new NewGroupDTO()
                {
                    Name = n.Name,
                    ShortName = n.ShortName,
                    Description = n.Description
                };

                var g = await _groupService.CreateGroupAsync(ng);

                //Add user as admin to newly created group
                await _groupService.AddMemberAsync(g.GroupId, long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), true);

                return Ok(g);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}"), Authorize("UpdateGroup")]
        public async Task<IActionResult> UpdateGroupAsync(long id, [FromBody] GroupUpdateResourceModel u)
        {
            try
            {
                UpdateGroupDTO ug = new UpdateGroupDTO()
                {
                    Name = u.Name,
                    ShortName = u.ShortName,
                    Description = u.Description
                };

                var g = await _groupService.UpdateGroupAsync(id, ug);

                return Ok(g);
            }
            catch (NotFoundException e)
            {
                return NotFound(e);
            }
        }

        [HttpGet("{id}/members"), Authorize("ViewGroups")]
        public async Task<IActionResult> GetMembersAsync(long id, [FromQuery] ListQueryArgs args)
        {
            try
            {
                var m = await _groupService.GetMembersAsync(id, args);

                return Ok(new ListResModel<MemberResourceModel>
                {
                    Paging = ListResHelper.GenerateHeaderFromPagedList(m),
                    Links = ListResHelper.GenerateLinksFromPagedList(Url, "GetGroups", m),
                    Items = m.List.Select(u => new MemberResourceModel()
                    {
                        UserId = u.User.UserId,
                        FirstName = u.User.FirstName,
                        LastName = u.User.LastName,
                        Title = u.User.Title,
                        IsGroupAdmin = u.IsGroupAdmin
                    }).ToList()
                });
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("{id}/members"), Authorize("UpdateGroup")]
        public async Task<IActionResult> PostMemberAsync(long id, [FromBody] MemberCreateResourceModel m)
        {
            try
            {
                var nm = await _groupService.AddMemberAsync(id, m.UserId, m.IsGroupAdmin);

                MemberResourceModel mr = new MemberResourceModel()
                {
                    UserId = nm.User.UserId,
                    FirstName = nm.User.FirstName,
                    LastName = nm.User.LastName,
                    Title = nm.User.Title,
                    IsGroupAdmin = nm.IsGroupAdmin
                };

                return Ok(mr);
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

        [HttpGet("{id}/members/{userId}"), Authorize("ViewGroups")]
        public async Task<IActionResult> GetMemberAsync(long id, long userId)
        {
            try
            {
                MemberDetailsDTO m = await _groupService.GetMemberAsync(id, userId);

                MemberDetailsResourceModel md = new MemberDetailsResourceModel()
                {
                    UserId = m.User.UserId,
                    FirstName = m.User.FirstName,
                    LastName = m.User.LastName,
                    Title = m.User.Title,
                    IsGroupAdmin = m.IsGroupAdmin,
                    Base64Picture = m.User.Picture != null ? Convert.ToBase64String(m.User.Picture) : null
                };

                return Ok(md);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}/members/{userId}"), Authorize("UpdateGroup")]
        public async Task<IActionResult> DeleteMemberAsync(long id, long userId)
        {
            try
            {
                await _groupService.RemoveMemberAsync(id, userId);

                return Ok();
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

        [HttpPut("{id}/members/{userId}"), Authorize("UpdateGroup")]
        public async Task<IActionResult> UpdateMemberAsync(long id, long userId, [FromBody] MemberUpdateResourceModel mu)
        {
            try
            {
                var mdto = new UpdateMemberDTO();
                mdto.IsGroupAdmin = mu.IsGroupAdmin;

                var m = await _groupService.UpdateMemberAsync(id, userId, mdto);

                return Ok(m);
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
    }
}