using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultCarType
    {
        public static void Seed(ApplicationDbContext context)
        {
            var models = context.CarTypes.ToList();
            if (models.Count == 0)
            {
                context.CarTypes.Add(new CarType { CarTypeName = "รถยนต์ 4 ประตู" });
                context.CarTypes.Add(new CarType { CarTypeName = "รถกะบะ" });
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
