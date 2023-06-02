using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultEmployer
    {
        public static void Seed(ApplicationDbContext context)
        {
            var models = context.Employers.ToList();
            if (models.Count == 0)
            {
                context.Employers.Add(new Employer { EmployerCode="EB",EmployerName="Easy buy" ,IsActive=true,UserCreated="admin",CreatedDateTime=DateTime.Now});
                context.Employers.Add(new Employer { EmployerCode="BBL",EmployerName= "ธนาคารกรุงเทพ", IsActive = true, UserCreated = "admin", CreatedDateTime = DateTime.Now });
                context.SaveChanges();
            }
        }
    }
}
