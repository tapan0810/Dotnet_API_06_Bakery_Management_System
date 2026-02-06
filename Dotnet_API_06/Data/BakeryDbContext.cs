using Dotnet_API_06.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_API_06.Data
{
    public class BakeryDbContext:DbContext
    {
        public BakeryDbContext(DbContextOptions<BakeryDbContext> options):base(options) { }

        public DbSet<Bakery> Bakeries => Set<Bakery>();
    }
}
