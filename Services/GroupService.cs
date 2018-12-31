using System.Linq;
using System.Threading.Tasks;
using hcm.Controllers;
using hcm.Database;
using hcm.Database.Models;
using hcm.DTOs.Groups;
using hcm.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace hcm.Services
{
    public class GroupService
    {
        private HcmContext _dbContext;
        private UserService _userService;

        public GroupService(HcmContext dbContext, UserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public PagedList<GroupDTO> GetGroups(ListQueryArgs args)
        {
            var query = _dbContext.Groups.Select(g => new GroupDTO(g));
            return new PagedList<GroupDTO>(query, args.PageNumber, args.PageSize);
        }

        public async Task<GroupDTO> GetGroupAsync(long id)
        {
            return new GroupDTO(await GetGroupModelAsync(id));
        }

        public async Task<GroupDTO> CreateGroupAsync(NewGroupDTO n)
        {
            Group g = new Group()
            {
                Name = n.Name,
                ShortName = n.ShortName,
                Description = n.Description
            };

            await _dbContext.Groups.AddAsync(g);
            await _dbContext.SaveChangesAsync();

            return new GroupDTO(g);
        }

        public async Task<GroupDTO> UpdateGroupAsync(long id, UpdateGroupDTO u)
        {
            var g = await GetGroupModelAsync(id);

            g.Name = u.Name;
            g.ShortName = u.ShortName;
            g.Description = u.Description;

            _dbContext.Groups.Update(g);
            await _dbContext.SaveChangesAsync();

            return new GroupDTO(g);
        }

        public async Task<PagedList<MemberDTO>> GetMembersAsync(long id, ListQueryArgs args)
        {
            var g = await GetGroupModelAsync(id);

            var query = _dbContext.GroupMemberships.Where(m => m.GroupId == id).Include(m => m.User).Select(m => new MemberDTO(m));
            return new PagedList<MemberDTO>(query, args.PageNumber, args.PageSize); //TODO: PagedList is not async
        }

        public async Task<MemberDTO> AddMemberAsync(long groupId, long userId, bool isAdmin)
        {
            var g = await GetGroupModelAsync(groupId);
            var u = await _userService.GetUserAsync(userId);

            var checkIfUserIsMemberQuery = _dbContext.GroupMemberships.Where(m => m.GroupId == groupId && m.UserId == userId);
            if ((await checkIfUserIsMemberQuery.FirstOrDefaultAsync()) != null)
                throw new BadRequestException("User is already a member of the group");

            var gm = new GroupMembership()
            {
                UserId = userId,
                GroupId = groupId,
                IsGroupAdmin = isAdmin
            };

            await _dbContext.GroupMemberships.AddAsync(gm);
            await _dbContext.SaveChangesAsync();

            return new MemberDTO(gm);
        }

        public async Task RemoveMemberAsync(long groupId, long userId)
        {
            var g = await GetGroupModelAsync(groupId);
            var u = await _userService.GetUserAsync(userId);

            var gm = await _dbContext.GroupMemberships.Where(m => m.GroupId == groupId && m.UserId == userId).FirstOrDefaultAsync();
            if (gm == null)
                throw new BadRequestException("User is not a member of the group");

            _dbContext.GroupMemberships.Remove(gm);
            await _dbContext.SaveChangesAsync();
        }

        private async Task<Group> GetGroupModelAsync(long id)
        {
            var g = await _dbContext.Groups.FindAsync(id);

            if (g == null)
                throw new NotFoundException("Group not found");

            return g;
        }
    }
}