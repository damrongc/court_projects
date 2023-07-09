using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultTaskStatus
    {
        public static void Seed(ApplicationDbContext context)
        {
            var models = context.LoanTaskStatuses.ToList();
            if (models.Count == 0)
            {
                context.LoanTaskStatuses.Add(new LoanTaskStatus { LoanTaskStatusId = 1, LoanTaskStatusName = "ติดต่อได้-นัดชำระ" });
                context.LoanTaskStatuses.Add(new LoanTaskStatus { LoanTaskStatusId = 2, LoanTaskStatusName = "ติดต่อได้-ไม่นัดชำระ" });
                context.LoanTaskStatuses.Add(new LoanTaskStatus { LoanTaskStatusId = 3, LoanTaskStatusName = "ติดต่อไม่ได้-หาตัวเจอ" });
                context.LoanTaskStatuses.Add(new LoanTaskStatus { LoanTaskStatusId = 4, LoanTaskStatusName = "ติดต่อไม่ได้-หาตัวไม่เจอ" });
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
