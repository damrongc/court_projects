using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
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
    public class TitleRepository : BaseRepository, ITitleRepository
    {
        public TitleRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }



        public async Task Create(Title model)
        {
            await Context.Titles.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.Titles.FindAsync(id);
            Context.Titles.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Title>> GetAll()
        {
            return await Context.Titles.ToListAsync();
        }

        public async Task<Title> GetByKey(string id)
        {
            var model = await Context.Titles.FindAsync(id);
            return model;
        }

        public bool IsExisting(string id)
        {
            return  Context.Titles.Any(p => p.TitleCode == id);
        }

        public async Task Update(string id, Title model)
        {
            var result = await Context.Titles.FindAsync(id);
            result.TitleName = model.TitleName;
            result.IsActive = model.IsActive;
            await Context.SaveChangesAsync();
        }
    }
}
