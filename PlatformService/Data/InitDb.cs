using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class InitDb
    {
        public static void InitData(IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices.CreateScope();
            var appDbContext = serviceScope.ServiceProvider.GetService<AppDbContext>();
            SeedData(appDbContext);
        }

        private static void SeedData(AppDbContext appDbContext)
        {
            if (appDbContext.Platforms.Any())
            {
                return;
            }

            appDbContext.Platforms.AddRangeAsync
            (
                new Platform {Name = "Test Name 1", Publisher = "Publisher 1", Cost = "Free"},
                new Platform {Name = "Test Name 2", Publisher = "Publisher 2", Cost = "Free"},
                new Platform {Name = "Test Name 3", Publisher = "Publisher 3", Cost = "Free"}
            );

            appDbContext.SaveChangesAsync();
        }
    }
}