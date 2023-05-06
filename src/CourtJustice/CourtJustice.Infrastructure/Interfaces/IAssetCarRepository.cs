using System;
using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IAssetCarRepository
	{
        Task<List<AssetCar>> GetAll();
        Task Create(AssetCar model);
        Task Update(int id, AssetCar model);
        Task Delete(string id);
        Task<AssetCar> GetByKey(string id);
    }
}

