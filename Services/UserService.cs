using System.IO;
using System.Linq;
using System.Threading.Tasks;
using hcm.Database;
using hcm.Database.Models;
using hcm.Exceptions;
using SixLabors.ImageSharp;

namespace hcm.Services
{
    public class UserService
    {
        private readonly HcmContext _dbContext;

        private readonly int MAX_WIDTH = 768;
        private readonly int MAX_HEIGHT = 960;

        public UserService(HcmContext context)
        {
            this._dbContext = context;
        }

        public async Task<User> GetUserAsync(long id)
        {
            var u = await _dbContext.Users.FindAsync(id);

            if (u == null)
                throw new NotFoundException("User not found");

            return u;
        }

        public async Task<User> SetPictureAsync(long id, byte[] imageBytes)
        {
            var u = await GetUserAsync(id);

            using (MemoryStream ms = new MemoryStream(imageBytes))
            using (var image = Image.Load(ms))
            using (MemoryStream outputms = new MemoryStream())
            {
                if (image.Height > MAX_HEIGHT || image.Width > MAX_WIDTH)
                    throw new BadRequestException("Image size can not exceed " + MAX_WIDTH + "x" + MAX_HEIGHT);

                image.SaveAsPng(outputms);

                u.Picture = outputms.ToArray();
            }

            u.IsPictureApproved = false;

            _dbContext.Update(u);
            await _dbContext.SaveChangesAsync();

            return u;
        }
    }
}