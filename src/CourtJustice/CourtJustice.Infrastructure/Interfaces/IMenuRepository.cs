using CourtJustice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IMenuRepository
    {
        Task<IEnumerable<AppProgram>> GetMenu(int GroupId);
    }
}
