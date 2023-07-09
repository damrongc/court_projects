using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IMenuRepository
    {
        Task<IEnumerable<AppProgram>> GetMenu(int GroupId);
    }
}
