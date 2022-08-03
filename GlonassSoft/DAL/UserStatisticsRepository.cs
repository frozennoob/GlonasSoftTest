using GlonassSoft.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace GlonassSoft.DAL
{
    public class UserStatisticsRepository : IUserStatisticsRepository
    {
        private readonly DataBaseContext _db;
        public UserStatisticsRepository(DataBaseContext db)
        {
            _db = db;
        }

        public async Task<Query> GetQuery(Guid queryGuid)
        {
            var result = await _db.Queries.FirstOrDefaultAsync(q => q.QueryId == queryGuid);

            return result;
        }

        public async Task<Guid> CreateQuery(Query query)
        {
            await _db.Queries.AddAsync(query);
            await _db.SaveChangesAsync();

            return query.QueryId;
        }

        public async Task UpdatePercent(Guid queryGuid, byte percent)
        {
            var query = await _db.Queries.FirstOrDefaultAsync(q => q.QueryId == queryGuid);
            query.Percent = percent;
            await _db.SaveChangesAsync();
        }

        public async Task UpdateSigninCount(Guid queryGuid, int signinCount)
        {
            var query = await _db.Queries.FirstOrDefaultAsync(q => q.QueryId == queryGuid);
            query.CountSignIn = signinCount;
            await _db.SaveChangesAsync();
        }
    }
}
