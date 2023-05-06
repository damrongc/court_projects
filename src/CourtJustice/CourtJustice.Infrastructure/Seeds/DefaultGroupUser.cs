using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultGroupUser
    {
        public static void Seed(ApplicationDbContext context)
        {
            var users = context.GroupUsers.ToList();
            if (users.Count == 0)
            {
                context.GroupUsers.Add(new GroupUser { GroupName = "Administrator", IsActive=true,UserCreated="admin",CreatedDateTime=DateTime.UtcNow });
                context.GroupUsers.Add(new GroupUser { GroupName = "Manager", IsActive=true, UserCreated = "admin", CreatedDateTime = DateTime.UtcNow });
                context.GroupUsers.Add(new GroupUser { GroupName = "Staff", IsActive= true, UserCreated = "admin", CreatedDateTime = DateTime.UtcNow });
                context.SaveChanges();
            }

        }
    }
}
