using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using hcm.Controllers;
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

        public PagedList<User> GetUsers(ListQueryArgs args)
        {
            var query = _dbContext.Users;
            return new PagedList<User>(query, args.PageNumber, args.PageSize);
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

        public string GenerateUsersZipFile()
        {
            var path = Path.GetTempPath() + "hcmusers";
            var zipPath = path + ".zip";

            if (Directory.Exists(path))
                Directory.Delete(path, true);

            Directory.CreateDirectory(path);

            foreach (var u in _dbContext.Users)
            {
                if (u.Picture == null)
                    continue;

                using (FileStream fs = new FileStream(path + "/" + u.UserId + ".jpg", FileMode.Create))
                {
                    fs.Write(u.Picture);
                }
            }

            //TODO: CSV file

            if (File.Exists(zipPath))
                File.Delete(zipPath);

            ZipFile.CreateFromDirectory(path, zipPath);

            return zipPath;
        }
    }
}