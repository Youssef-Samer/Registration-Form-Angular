using CitizensAPI.Core.Entities;

namespace CitizensAPI.Core.Interfaces
{
    public interface ICitizenRepository
    {
        Task<IEnumerable<CitizenDetails>> GetAllAsync();
        Task<CitizenDetails?> GetByIdAsync(Guid id);
        Task AddAsync(CitizenDetails citizenDetails);
        Task UpdateAsync(CitizenDetails citizenDetails);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
