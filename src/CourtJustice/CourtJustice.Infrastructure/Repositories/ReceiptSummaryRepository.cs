using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Infrastructure.Repositories
{
    public class ReceiptSummaryRepository : BaseRepository, IReceiptSummaryRepository
    {
        public ReceiptSummaryRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public async Task AddOrUpdateReceipt(ReceiptSummary receipt)
        {
            try
            {
                var model =await Context.ReceiptSummaries.Where(p => p.EmployerCode == receipt.EmployerCode
                && p.CollectorId==receipt.CollectorId
                && p.ReceiptMonth==receipt.ReceiptMonth
                && p.ReceiptYear==receipt.ReceiptYear).SingleOrDefaultAsync();

                if (model == null)
                {
                    await Context.ReceiptSummaries.AddAsync(receipt); 
                }
                else
                {
                    model.TotalAmont += receipt.TotalAmont;
                    model.Fee += receipt.Fee;
                }
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteReceipt(ReceiptSummary receipt)
        {
            try
            {
                var model = await Context.ReceiptSummaries.Where(p => p.EmployerCode == receipt.EmployerCode
                && p.CollectorId == receipt.CollectorId
                && p.ReceiptMonth == receipt.ReceiptMonth
                && p.ReceiptYear == receipt.ReceiptYear).SingleOrDefaultAsync();

                if (model != null)
                {
                    model.TotalAmont -= receipt.TotalAmont;
                    model.Fee -= receipt.Fee;
                    await Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
