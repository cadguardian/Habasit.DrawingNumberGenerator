using Microsoft.EntityFrameworkCore;

namespace Client.Data
{
    public class PartNumberDbContext : DbContext
    {
        public PartNumberDbContext(DbContextOptions<PartNumberDbContext> options)
            : base(options) { }

        public DbSet<PartNumber> PartNumbers { get; set; }
    }
}