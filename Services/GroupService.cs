using System.Linq;
using System.Threading.Tasks;
using hcm.Controllers;
using hcm.Database;
using hcm.Database.Models;
using hcm.Exceptions;

namespace hcm.Services
{
    public class GroupService
    {
        private HcmContext _dbContext;

        public GroupService(HcmContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PagedList<Group> GetGroups(ListQueryArgs args)
        {
            var query = _dbContext.Groups;
            return new PagedList<Group>(query, args.PageNumber, args.PageSize);
        }

        public async Task<Group> GetGroupAsync(long id)
        {
            var g = await _dbContext.Groups.FindAsync(id);

            if (g == null)
                throw new NotFoundException("Group not found");

            return g;
        }

        public async Task<PagedList<User>> GetMembersAsync(long id, ListQueryArgs args)
        {
            var g = await GetGroupAsync(id);

            var query = _dbContext.GroupMemberships.Where(m => m.GroupId == id).Select(m => m.User);
            return new PagedList<User>(query, args.PageNumber, args.PageSize);
        }
    }
}