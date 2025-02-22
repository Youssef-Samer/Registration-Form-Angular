using Microsoft.EntityFrameworkCore;

namespace CitizensAPI.Models
{
    public class CitizenDetailsContext : DbContext
    {
        public CitizenDetailsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CitizenDetails> CitizenDetails { get; set; }
    }
}
