using System;
using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
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

        public async Task<Payment> GetByKey(int id)
        {
            var model = await Context.Payments.FindAsync(id);
            return model;
        }

        public async Task Update(int id, Payment model)
        {
            var result = await Context.Payments.FindAsync(model.PaymentId);
            result.LoanNumber = model.LoanNumber;
            result.PaymentSeq = model.PaymentSeq;
            result.PaymentDate = model.PaymentDate;
            result.Amount = model.Amount;
            result.Fee = model.Fee;


            await Context.SaveChangesAsync();
        }
    }
}

