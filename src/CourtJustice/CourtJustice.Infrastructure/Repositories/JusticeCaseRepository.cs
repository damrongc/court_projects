using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;
using System.Text;

namespace CourtJustice.Infrastructure.Repositories
{
    public class JusticeCaseRepository : BaseRepository, IJusticeCaseRepository
    {
        public JusticeCaseRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(JusticeCase model)
        {
            //var justiceCase = new JusticeCase
            //{
            //    BlackCaseNo = model.BlackCaseNo,
            //    CaseDate = model.CaseDate,
            //    ApprovalDate = model.ApprovalDate,
            //    JudgmentDate = model.JudgmentDate,
            //    AssetAmount = model.AssetAmount,
            //    CaseDocumentResult = model.CaseDocumentResult,
            //    FeeCase = model.FeeCase,
            //    SubmissionDate = model.SubmissionDate,
            //    SubmissionResult = model.SubmissionResult,
            //    CommitDate = model.CommitDate,
            //    PostingDate = model.PostingDate,
            //    CaseResultId = model.CaseResultId,
            //    CourtId = model.CourtId,
            //    CusId = model.CusId,
            //};
            await Context.JusticeCases.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<JusticeCaseViewModel>> GetAll()
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select jus.*,loanee.name as cus_name ,c.case_result_name  from justice_case jus
left outer join loanee 
on loanee.cus_id  = jus.cus_id
left outer join case_result c
on jus.case_result_id = c.case_result_id ";
                var result = await conn.QueryAsync<JusticeCaseViewModel>(sql);
                return result.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<JusticeCaseViewModel> GetByCusId(string cusId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select * from justice_case where cus_id=@cus_id";
                var result = await conn.QueryAsync<JusticeCaseViewModel>(sql, new { cus_id = cusId });
                return result.FirstOrDefault()!;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<JusticeCaseViewModel> GetByKey(string id)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select * from justice_case where black_case_no=@black_case_no";
                var result = await conn.QueryAsync<JusticeCaseViewModel>(sql, new { black_case_no = id });
                return result.FirstOrDefault()!;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<JusticeCaseViewModel>> GetPaging(string courtId, int caseResultId, int skip, int take, string filter)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("th-TH");
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select jus.*,loanee.name as cus_name ,c.case_result_name , court.court_name  from justice_case jus
left outer join loanee 
on loanee.cus_id  = jus.cus_id
left outer join case_result c
on jus.case_result_id = c.case_result_id
left outer join court
on court.court_id = jus.court_id
where 1=1 ";
                var sb = new StringBuilder();
                sb.Append(sql);
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (jus.cus_id LIKE @filter");
                    sb.Append(" or jus.black_case_no LIKE @filter");
                    sb.Append(" )");
                }
                if (!string.IsNullOrEmpty(courtId))
                {
                    sb.Append(" and jus.court_id=@bucketId");
                }
                if (caseResultId > 0)
                {
                    sb.Append(" and jus.case_result_id=@case_result_id");
                }

                sb.Append(" Limit @skip,@take");
                var dictionary = new Dictionary<string, object>
                    {
                         { "@skip", skip },
                         { "@take", take },
                    };
                if (!string.IsNullOrEmpty(filter))
                {
                    dictionary.Add("@filter", string.Format("%{0}%", filter));
                }
                if (!string.IsNullOrEmpty(courtId))
                {
                    dictionary.Add("@court_id", courtId);
                }
                if (caseResultId > 0)
                {
                    dictionary.Add("@case_result_id", caseResultId);
                }
                var parameters = new DynamicParameters(dictionary);
                var result = await conn.QueryAsync<JusticeCaseViewModel>(sb.ToString(), parameters);
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> GetRecordCount(string courtId, int caseResultId, string filter)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();

                sb.Append("select count(1) from justice_case where 1=1");
                if (!string.IsNullOrEmpty(filter))
                {
                    sb.Append(" and (cus_id LIKE @filter");
                    sb.Append(" or black_case_no LIKE @filter");
                    sb.Append(" )");
                }
                if (!string.IsNullOrEmpty(courtId))
                {
                    sb.Append(" and court_id=@bucketId");
                }
                if (caseResultId > 0)
                {
                    sb.Append(" and case_result_id=@case_result_id");
                }
                var dictionary = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(filter))
                {
                    dictionary.Add("@filter", string.Format("%{0}%", filter));
                }
                if (!string.IsNullOrEmpty(courtId))
                {
                    dictionary.Add("@court_id", courtId);
                }
                if (caseResultId > 0)
                {
                    dictionary.Add("@case_result_id", caseResultId);
                }
                var parameters = new DynamicParameters(dictionary);

                var rowCount = await conn.ExecuteScalarAsync<int>(sb.ToString(), parameters);
                return rowCount;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task Update(string id, JusticeCase model)
        {
            throw new NotImplementedException();
        }
    }
}
