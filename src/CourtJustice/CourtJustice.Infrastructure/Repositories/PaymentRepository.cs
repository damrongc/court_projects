using System;
using System.Data;
using System.Text;
using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace CourtJustice.Infrastructure.Repositories
{
    public class PaymentRepository : BaseRepository, IPaymentRepository

    {
        public PaymentRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task Create(Payment model)
        {
            await Context.Payments.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await Context.Payments.FindAsync(id);
            Context.Payments.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Payment>> GetAll()
        {
            return await Context.Payments.ToListAsync();
        }

        public async Task<List<PaymentViewModel>> GetByCusId(string id)
        {
            {
                try
                {
                    using IDbConnection conn = Connection;
                    conn.Open();
                    var sb = new StringBuilder();
                    sb.Append("select * from payment where cus_id=@cus_id");
                    var result = await conn.QueryAsync<PaymentViewModel>(sb.ToString(), new { cus_id = id });
                    return result.ToList();

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public async Task<Payment> GetByKey(int id)
        {
            var model = await Context.Payments.FindAsync(id);
            return model;
        }

        //public async Task<IEnumerable<AssetLandViewModel>> GetPaging(int skip, int take, string filter)
        //{
        //    try
        //    {
        //        using IDbConnection conn = Connection;
        //        conn.Open();
        //        var sb = new StringBuilder();
        //        sb.Append("select payment_id, loan_number, payment_seq,payment_date,amount, free" +
        //            " from payments" );
        //        if (!string.IsNullOrEmpty(filter))
        //        {
        //            sb.Append(" where (payment_id LIKE @filter");
        //            sb.Append(" or loan_number  @filter");
        //            sb.Append(" )");
        //        }
        //        sb.Append(" Limit @skip,@take");
        //        var dictionary = new Dictionary<string, object>
        //            {
        //                 { "@skip", skip },
        //                 { "@take", take },
        //            };
        //        if (!string.IsNullOrEmpty(filter))
        //        {
        //            dictionary.Add("@filter", string.Format("%{0}%", filter));
        //        }
        //        var parameters = new DynamicParameters(dictionary);
        //        var result = await conn.QueryAsync<AssetLandViewModel>(sb.ToString(), parameters);
        //        return result;

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public async Task<int> GetPaymentSeq(string cusId)
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sb = new StringBuilder();

                sb.Append("select max(payment_seq) from payment where cus_id=@cus_id");
                var seq = await conn.ExecuteScalarAsync<int>(sb.ToString(), new { cus_id = cusId });
                return seq+1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<int> GetRecordCount(string filter)
        //{
        //    try
        //    {
        //        using IDbConnection conn = Connection;
        //        conn.Open();
        //        var sb = new StringBuilder();

        //        sb.Append("select count(1) from payments ");
        //        if (!string.IsNullOrEmpty(filter))
        //        {
        //            sb.Append(" where (payment_id LIKE @filter");
        //            sb.Append(" or loan_number LIKE @filter");
        //            sb.Append(" )");
        //        }
        //        var dictionary = new Dictionary<string, object>();

        //        if (!string.IsNullOrEmpty(filter))
        //        {
        //            dictionary.Add("@filter", string.Format("%{0}%", filter));
        //        }
        //        var parameters = new DynamicParameters(dictionary);

        //        var rowCount = await conn.ExecuteScalarAsync<int>(sb.ToString(), parameters);
        //        return rowCount;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public bool IsExisting(int id)
        {
            return Context.Payments.Any(p => p.PaymentId == id);
        }

        public async Task Update(int id, Payment model)
        {
            var result = await Context.Payments.FindAsync(model.PaymentId);
            result.PaymentDate = model.PaymentDate;
            result.Amount = model.Amount;
            result.Fee = model.Fee;

            await Context.SaveChangesAsync();
        }
    }
}

