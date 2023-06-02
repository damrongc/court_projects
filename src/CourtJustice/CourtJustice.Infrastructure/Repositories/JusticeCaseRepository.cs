using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

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

        public async Task Update(string id, JusticeCase model)
        {
            throw new NotImplementedException();
        }
    }
}
