using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultLoanType
    {
        public static void Seed(ApplicationDbContext context)
        {
            var models = context.LoanTypes.ToList();
            if (models.Count == 0)
            {
                context.LoanTypes.Add(new LoanType { LoanTypeCode = "00", LoanTypeName = "ไม่ระบุ", IsActive = true, CreatedDateTime = DateTime.Now, UserCreated = "admin" });
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
