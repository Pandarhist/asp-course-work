using Microsoft.EntityFrameworkCore;
using MusicShop.Models;

namespace MusicShop.DbContexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
