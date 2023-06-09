﻿using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultGroupUser
    {
        public static void Seed(ApplicationDbContext context)
        {
            var users = context.GroupUsers.ToList();
            if (users.Count == 0)
            {
                context.GroupUsers.Add(new GroupUser { GroupName = "ผู้ดูแลระบบ", IsActive = true,GroupLevel=99, UserCreated = "admin", CreatedDateTime = DateTime.Now });
                context.GroupUsers.Add(new GroupUser { GroupName = "ผู้จัดการ", IsActive = true, GroupLevel = 1, UserCreated = "admin", CreatedDateTime = DateTime.Now });
                context.GroupUsers.Add(new GroupUser { GroupName = "ทนายความ", IsActive = true, GroupLevel = 1, UserCreated = "admin", CreatedDateTime = DateTime.Now });
                context.GroupUsers.Add(new GroupUser { GroupName = "พนักงาน", IsActive = true, GroupLevel = 0, UserCreated = "admin", CreatedDateTime = DateTime.Now });
                context.SaveChanges();
            }

        }
    }
}
