using CourtJustice.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        protected BaseRepository(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        public IDbConnection Connection
        {
            get
            {
                //return new NpgsqlConnection(_config.GetConnectionString("PostgresConnection"));
                return new MySqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public ApplicationDbContext Context
        {
            get
            {
                return _context;
            }
        }
    }
}
