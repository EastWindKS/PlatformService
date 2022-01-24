using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data.Repositories
{
    public class PlatformRepository : IPlatformRepository
    {
        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        }

        private readonly AppDbContext _context;

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<IEnumerable<Platform>> GetAll()
        {
            return await _context.Platforms.ToListAsync();
        }

        public async Task<Platform> GetById(int id)
        {
            return await _context.Platforms.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreatePlatform(Platform platform)
        {
            await _context.Platforms.AddAsync(platform);
            await _context.SaveChangesAsync();
        }
    }
}