using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class MenuRepository : BaseRepository, IMenuRepository
    {
        public MenuRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task<IEnumerable<AppProgram>> GetMenu(int GroupId)
        {
            try
            {

                using IDbConnection conn = Connection;
                conn.Open();
                if (GroupId == 1)
                {
                    var sql = @"SELECT * FROM app_program ORDER BY program_id,parent_program_id ASC";
                    var appPrograms = await conn.QueryAsync<AppProgram>(sql);
                    return appPrograms;
                }
                else
                {
                    var sql = @"SELECT 
    per.program_id,
    program_name,
    parent_program_id,
    controller_name,
    action_name,
    menu_icon
FROM
    user_permission per,
    app_program pg
 WHERE
    per.program_id = pg.program_id
    AND per.group_id = @GroupId
ORDER BY per.program_id,parent_program_id ASC";
                    var appPrograms = await conn.QueryAsync<AppProgram>(sql, new { GroupId });
                    return appPrograms;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
