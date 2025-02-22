using CitizensAPI.Core.Interfaces;

namespace CitizensAPI.Data.Repositories
{
    public class CitizenRepositoryFactory : ICitizenRepositoryFactory
    {
        private readonly CitizenDetailsContext _context;

        public CitizenRepositoryFactory(CitizenDetailsContext context)
        {
            _context = context;
        }

        public ICitizenRepository Create()
        {
            return new CitizenRepository(_context);
        }
    }
}
