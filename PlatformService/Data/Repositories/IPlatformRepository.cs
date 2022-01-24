using System.Collections.Generic;
using System.Threading.Tasks;
using PlatformService.Models;

namespace PlatformService.Data.Repositories
{
    public interface IPlatformRepository
    {
        Task<bool> SaveChanges();

        Task<IEnumerable<Platform>> GetAll();

        Task<Platform> GetById(int id);

        Task CreatePlatform(Platform platform);
    }
}