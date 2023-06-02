using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IJusticeAppointmentRepository
    {
        Task Create(JusticeAppointment model);
        Task<List<JusticeAppointmentViewModel>> GetByCaseNo(string id);
        Task<JusticeAppointmentViewModel> GetLastByCaseNo(string id);
    }
}
