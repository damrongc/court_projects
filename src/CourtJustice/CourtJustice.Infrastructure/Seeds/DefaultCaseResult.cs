using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultCaseResult
    {
        public static void Seed(ApplicationDbContext context)
        {
            var users = context.CaseResults.ToList();
            if (users.Count == 0)
            {
                context.CaseResults.Add(new CaseResult
                {
                    CaseResultName = "เลื่อน",
                });

                context.CaseResults.Add(new CaseResult
                {
                    CaseResultName = "พิพากษา",
                });
                context.CaseResults.Add(new CaseResult
                {
                    CaseResultName = "ทำยอม",
                });
                context.CaseResults.Add(new CaseResult
                {
                    CaseResultName = "ถอนฟ้อง ",
                });
                context.SaveChanges();
            }

        }
    }
}
