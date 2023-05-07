using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Seeds
{
    public static class DefaultBucket
    {
        public static void Seed(ApplicationDbContext context)
        {
            var models = context.Buckets.ToList();
            if (models.Count == 0)
            {
                context.Buckets.Add(new Bucket { BucketId=1,BucketName= "ก่อนฟ้อง" });
                context.Buckets.Add(new Bucket { BucketId=2,BucketName= "ระหว่างพิจารณาคดี" });
                context.Buckets.Add(new Bucket { BucketId=3,BucketName= "มีหมายบังคับแล้ว" });
                context.Buckets.Add(new Bucket { BucketId=4,BucketName= "หมดอายุความ" });
                context.Buckets.Add(new Bucket { BucketId=5,BucketName= "ปิดบัญชี" });
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
