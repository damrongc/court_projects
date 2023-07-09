using CourtJustice.Domain.Models;
using CourtJustice.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CourtJustice.Infrastructure.Repositories
{
    public class CardTypeRepository : BaseRepository, ICardTypeRepository
    {
        public CardTypeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)

        {
        }

        public async Task Create(CardType model)
        {
            await Context.CardTypes.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.CardTypes.FindAsync(id);
            Context.CardTypes.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<CardType>> GetAll()
        {
            return await Context.CardTypes.ToListAsync();
        }

        public async Task<CardType> GetByKey(string id)
        {
            var model = await Context.CardTypes.FindAsync(id);
            return model;
        }

        public async Task Update(string id, CardType model)
        {
            var result = await Context.CardTypes.FindAsync(model.CardTypeCode);
            result.CardTypeName = model.CardTypeName;
            result.IsActive = model.IsActive;

            await Context.SaveChangesAsync();
        }
    }
}

