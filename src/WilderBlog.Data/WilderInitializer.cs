using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WilderBlog.Data
{
    public class WilderInitializer
    {
        private WilderContext _ctx;
        private UserManager<WilderUser> _userMgr;

        public WilderInitializer(WilderContext ctx, UserManager<WilderUser> userMgr)
        {
            _ctx = ctx;
            _userMgr = userMgr;
        }

        public async Task SeedAsync()
        {
            // Seed User
            if (await _userMgr.FindByNameAsync("saschamanns") == null)
            {
                var user = new WilderUser()
                {
                    Email = "Sascha.Manns@outlook.de",
                    UserName = "saschamanns",
                    EmailConfirmed = true
                };

                var result = await _userMgr.CreateAsync(user, "@Passw0rd"); // Temp Password
                if (!result.Succeeded) throw new InvalidProgramException("Failed to create seed user");
            }

            // Seed Stories
            if (!_ctx.Stories.Any())
            {
                var stories = MemoryRepository._stories;
                stories.ForEach(s => s.Id = 0);
                _ctx.Stories.AddRange(stories);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}
