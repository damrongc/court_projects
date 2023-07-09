using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;

namespace CourtJustice.Infrastructure.Repositories
{
    public class RemainTaskRepository : BaseRepository, IRemainTaskRepository
    {
        public RemainTaskRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(RemainTask model)
        {
            await Context.RemainTasks.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var task = await Context.RemainTasks.FindAsync(id);
            Context.RemainTasks.Remove(task);
            await Context.SaveChangesAsync();
        }

        public async Task<List<RemainTask>> GetAll()
        {
            return await Context.RemainTasks.ToListAsync();
        }

//        public async Task<IEnumerable<RemainTaskViewModel>> GetPaging(string employeeCode, int skip, int take, string filter)
//        {
//            try
//            {
//                using IDbConnection conn = Connection;
//                conn.Open();
//                var sql = @"SELECT task_id,task_datetime,task_detail,assign_to,assign_from,user_name as assign_from_name
//FROM remain_task a,app_user b WHERE a.assign_from =b.user_id";
//                var sb = new StringBuilder();
//                sb.Append(sql);
//                if (!string.IsNullOrEmpty(filter))
//                {
//                    sb.Append(" and (task_detail LIKE @filter");
//                    sb.Append(" or assign_to LIKE @filter");
//                    sb.Append(" )");
//                }

//                if (!string.IsNullOrEmpty(employeeCode))
//                {
//                    sb.Append(" and assign_to=@employeeCode");
//                }
//                sb.Append(" order by task_datetime desc");
//                sb.Append(" Limit @skip,@take");
//                var dictionary = new Dictionary<string, object>
//                    {
//                         { "@skip", skip },
//                         { "@take", take },
//                    };
//                if (!string.IsNullOrEmpty(filter))
//                {
//                    dictionary.Add("@filter", string.Format("%{0}%", filter));
//                }

//                if (!string.IsNullOrEmpty(employeeCode))
//                {
//                    dictionary.Add("@employeeCode", employeeCode);
//                }

//                var parameters = new DynamicParameters(dictionary);
//                var result = await conn.QueryAsync<RemainTaskViewModel>(sb.ToString(), parameters);
//                return result;

//            }
//            catch (Exception)
//            {

//                throw;
//            }
//        }

//        public async Task<int> GetRecordCount(string employeeCode, string filter)
//        {
//            try
//            {
//                using IDbConnection conn = Connection;
//                conn.Open();
//                var sb = new StringBuilder();

//                sb.Append("SELECT count(1) FROM remain_task a,app_user b WHERE a.assign_from =b.user_id");
//                if (!string.IsNullOrEmpty(filter))
//                {
//                    sb.Append(" and (task_detail LIKE @filter");
//                    sb.Append(" or assign_to LIKE @filter");
//                    sb.Append(" )");
//                }

//                if (!string.IsNullOrEmpty(employeeCode))
//                {
//                    sb.Append(" and assign_to=@employeeCode");
//                }


//                var dictionary = new Dictionary<string, object>();
//                if (!string.IsNullOrEmpty(filter))
//                {
//                    dictionary.Add("@filter", string.Format("%{0}%", filter));
//                }

//                if (!string.IsNullOrEmpty(employeeCode))
//                {
//                    dictionary.Add("@employeeCode", employeeCode);
//                }

//                var parameters = new DynamicParameters(dictionary);
//                var rowCount = await conn.ExecuteScalarAsync<int>(sb.ToString(), parameters);
//                return rowCount;
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }

        public async Task Update(int id, RemainTask model)
        {
            var task = await Context.RemainTasks.FindAsync(id);
            task.TaskDetail = model.TaskDetail;
            task.AssignTo = model.AssignTo;
            await Context.SaveChangesAsync();
        }
    }
}
