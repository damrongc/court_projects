using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultLoanee
    {
        public static void Seed(ApplicationDbContext context)
        {
            var models = context.Loanees.ToList();
            if (models.Count == 0)
            {
                context.Loanees.Add(new Loanee
                {
                    CusId = "05512-94337-2",
                    AssignDate = DateOnly.FromDateTime(DateTime.Today),
                    ExpireDate = DateOnly.FromDateTime(DateTime.Today),
                    BirthDate = DateOnly.FromDateTime(DateTime.Today),
                    NationalityId = "32121124654551",
                    Name = "ขวัญเรือน บุญมา",
                    TelephoneHome = "0832XXXX",
                    OccupationId = 1,
                    ContractNo = "630807",
                    ContractDate = DateOnly.FromDateTime(DateTime.Today),
                    Term = 20,
                    LoanTypeCode="IR",
                    IsActive = true,
                    CreatedDateTime = DateTime.Now,
                    UserCreated = "system"

                }) ;
     
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
