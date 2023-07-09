using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultUser
    {
        public static void Seed(ApplicationDbContext context)
        {
            var users = context.AppUsers.ToList();
            if (users.Count == 0)
            {
                context.AppUsers.Add(new AppUser
                {
                    UserId = "admin",
                    UserName = "ผู้ดูแลระบบ",
                    Password = "fORRNGKa8yYj1WaiuGE079GcOFcTadWiDkwa7Hzd0yg=",
                    Email = "admin@court.com",
                    PhoneNumber = "",
                    GroupId = 1,
                    IsActive = true,
                    UserCreated = "admin",
                    CreatedDateTime = DateTime.UtcNow

                });
                context.SaveChanges();
            }

        }
    }
}
