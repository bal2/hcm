using System.Security.Claims;
using System.Threading.Tasks;
using hcm.DTOs.Groups;
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

        [HttpGet(Name = "GetGroups"), Authorize]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupAsync(long id)
        {
            var g = await _groupService.GetGroupAsync(id);

            return Ok(g);
        }

        [HttpPost, Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> PostGroupAsync([FromBody] GroupCreateResourceModel n)
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

        
    }
}