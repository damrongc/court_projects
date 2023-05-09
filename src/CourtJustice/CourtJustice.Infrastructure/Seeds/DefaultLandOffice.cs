using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultLandOffice
    {
        public static void Seed(ApplicationDbContext context)
        {
            var models = context.LandOffices.ToList();
            if (models.Count == 0)
            {
                context.LandOffices.Add(new LandOffice { LandOfficeCode = "01", LandOfficeName = "สำนักงานที่ดินจังหวัดนนทบุรี", IsActive = true, UserCreated = "System", CreatedDateTime = DateTime.Now });
                context.LandOffices.Add(new LandOffice { LandOfficeCode = "02", LandOfficeName = "สำนักงานที่ดินจังหวัดปราจีนบุรี", IsActive = true, UserCreated = "System", CreatedDateTime = DateTime.Now });

                context.SaveChanges();
            }

            //var models1 = context.LoanSubTaskStatuses.ToList();
            //if (models1.Count == 0)
            //{
            //    context.LoanSubTaskStatuses.Add(new LoanSubTaskStatus { LoanTaskStatusId = 1, LoanSubTaskStatusId = 1 ,LoanSubTaskStatusName= "นัดชำระ" });
            //    context.LoanSubTaskStatuses.Add(new LoanSubTaskStatus { LoanTaskStatusId = 1, LoanSubTaskStatusId = 2 ,LoanSubTaskStatusName= "ไม่นัดชำระ" });
            //    context.LoanSubTaskStatuses.Add(new LoanSubTaskStatus { LoanTaskStatusId = 2, LoanSubTaskStatusId = 3 ,LoanSubTaskStatusName="หาตัวเจอ" });
            //    context.LoanSubTaskStatuses.Add(new LoanSubTaskStatus { LoanTaskStatusId = 2, LoanSubTaskStatusId = 3 ,LoanSubTaskStatusName= "หาตัวไม่เจอ" });
            //    context.SaveChanges();
            //}

        }
    }
}
