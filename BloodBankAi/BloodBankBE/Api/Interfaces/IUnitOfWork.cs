using Api.Data.Entities;
using Api.Data.Entities.Identity;

namespace Api.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<ApplicationUser> Users { get; }
        IBaseRepository<Bank> Banks { get; }
        IBaseRepository<Governorate> Governorates { get; }
        IBaseRepository<City> Cities { get; }
        IBaseRepository<Address> Adresses { get; }
        IBaseRepository<Moderator> Moderators { get; }
        IBaseRepository<BloodGroup> BloodGroups { get; }

        int Complete();
    }
}
