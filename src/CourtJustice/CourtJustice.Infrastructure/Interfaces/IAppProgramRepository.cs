using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IAppProgramRepository
    {
        Task<List<AppProgram>> GetAll();
        Task<AppProgram> GetById(int id);
    }
}
