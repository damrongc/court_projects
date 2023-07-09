using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultLawyer
    {
        public static void Seed(ApplicationDbContext context)
        {
            var lawyers = context.Lawyers.ToList();
            if (lawyers.Count == 0)
            {
                context.Lawyers.Add(new Lawyer
                {
                    LawyerName = "ทนายเอ",
                });
                context.Lawyers.Add(new Lawyer
                {
                    LawyerName = "ทนายศักดา",
                });

                context.SaveChanges();
            }

        }
    }
}
