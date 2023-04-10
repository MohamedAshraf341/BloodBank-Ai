using Api.Data.Entities;
using Api.Data.Entities.Identity;
using Api.Interfaces;
using System.Net;

namespace Api.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<ApplicationUser> Users { get; private set; }
        public IBaseRepository<Bank> Banks { get; private set; }

        public IBaseRepository<Governorate> Governorates { get; private set; }

        public IBaseRepository<City> Cities { get; private set; }

        public IBaseRepository<Address> Adresses { get; private set; }

        public IBaseRepository<Moderator> Moderators { get; private set; }

        public IBaseRepository<BloodGroup> BloodGroups { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new BaseRepository<ApplicationUser>(_context);
            Banks = new BaseRepository<Bank>(_context);
            Governorates = new BaseRepository<Governorate>(_context);
            Cities = new BaseRepository<City>(_context);
            Adresses = new BaseRepository<Address>(_context);
            Moderators = new BaseRepository<Moderator>(_context);
            BloodGroups = new BaseRepository<BloodGroup>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
