using CitizensAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitizensAPI.Data
{
    public class CitizenDetailsContext : DbContext
    {
        public CitizenDetailsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CitizenDetails> CitizenDetails { get; set; }
    }
}
