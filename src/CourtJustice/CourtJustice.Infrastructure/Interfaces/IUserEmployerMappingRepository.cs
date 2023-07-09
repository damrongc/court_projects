using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IUserEmployerMappingRepository
    {
        Task Create(UserEmployerMapping model);

        List<UserEmployerMapping?> GetByUser(string userId);
        List<UserEmployerMapping?> GetByEmployer(string employerCode);

        void DeleteByUser(string userId);
    }
}
