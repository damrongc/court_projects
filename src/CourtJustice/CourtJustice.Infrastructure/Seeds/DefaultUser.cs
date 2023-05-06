using CourtJustice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    UserId= "admin",
                    UserName= "Administrator",
                    Password= "fORRNGKa8yYj1WaiuGE079GcOFcTadWiDkwa7Hzd0yg=",
                    Email= "admin@inventor.com",
                    GroupId=1,
                    IsActive=true,
                    UserCreated = "admin",
                    CreatedDateTime = DateTime.UtcNow

                });
               context.SaveChanges();
            }

        }
    }
}
