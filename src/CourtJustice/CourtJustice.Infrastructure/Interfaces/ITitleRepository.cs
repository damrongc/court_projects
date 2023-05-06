﻿using CourtJustice.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface ITitleRepository
    {
        Task<List<Title>> GetAll();
        Task Create(Title model);
        Task Update(string id, Title model);
        Task Delete(string id);
        Task<Title> GetByKey(string id);

       bool IsExisting(string id);

       
    }
}
