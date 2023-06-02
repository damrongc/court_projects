using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultCompany
    {
        public static void Seed(ApplicationDbContext context)
        {
            var models = context.Companys.ToList();
            if (models.Count == 0)
            {
                context.Companys.Add(new Company {
                 CompanyId= 1,
                 CompanyName= "บริษัท เอ็ม.บี.ลีกัล แอนด์ บิซิเนส จำกัด",
                 Address= "เลขที่ 190/51 หมู่ 2 ตำบลพิมลราช อำเภอบางบัวทอง จังหวัดนนทบุรี 11110",
                 PhoneNumber="",
                 IsActive=true,
                 CreatedDateTime= DateTime.Now,
                 UserCreated="system",
                 LogoPath="company_logo.png"
                });
                context.SaveChanges();
            }
        }
    }
}
