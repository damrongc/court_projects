using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultOccupation
    {
        public static void Seed(ApplicationDbContext context)
        {
            var occupations = context.Occupations.ToList();
            if (occupations.Count == 0)
            {
                context.Occupations.Add(new Occupation
                {
                    OccupationId = 1,
                    OccupationName = "ไม่ระบุ",

                });
                context.Occupations.Add(new Occupation
                {
                    OccupationId = 2,
                    OccupationName = "แม่บ้าน",

                });
                context.Occupations.Add(new Occupation
                {
                    OccupationId = 3,
                    OccupationName = "พนักงานบริษัท",

                });
                context.SaveChanges();
            }

        }
    }
}
