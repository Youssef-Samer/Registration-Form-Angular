using CitizensAPI.Core.Entities;
using CitizensAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CitizensAPI.Infrastructure.Persistence.Repositories
{
    public class CitizenRepository : ICitizenRepository
    {
        private readonly CitizenDetailsContext _context;

        public CitizenRepository(CitizenDetailsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CitizenDetails>> GetAllAsync()
        {
            return await _context.CitizenDetails.ToListAsync();
        }

        public async Task<CitizenDetails?> GetByIdAsync(Guid id)
        {
            return await _context.CitizenDetails.FindAsync(id);
        }

        public async Task AddAsync(CitizenDetails citizenDetails)
        {
            await _context.CitizenDetails.AddAsync(citizenDetails);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CitizenDetails citizenDetails)
        {
            _context.Entry(citizenDetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var citizenDetails = await _context.CitizenDetails.FindAsync(id);
            if (citizenDetails == null) return false;

            _context.CitizenDetails.Remove(citizenDetails);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.CitizenDetails.AnyAsync(e => e.CitizenId == id);
        }
    }
}
