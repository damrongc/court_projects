using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultCourt
    {
        public static void Seed(ApplicationDbContext context)
        {
            var users = context.Courts.ToList();
            if (users.Count == 0)
            {
                context.Courts.Add(new Court
                {
                    CourtId = "100345",
                    CourtName = "ศาลแพ่ง",
                });
                context.Courts.Add(new Court
                {
                    CourtId = "100353",
                    CourtName = "ศาลแพ่งกรุงเทพใต้",
                });
                context.SaveChanges();
            }

        }
    }
}
