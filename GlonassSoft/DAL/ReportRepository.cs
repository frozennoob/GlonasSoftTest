using GlonassSoft.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlonassSoft.DAL
{
    public class ReportRepository : IReportRepository
    {
        private readonly DataBaseContext _db;
        public ReportRepository(DataBaseContext db)
        {
            _db = db;
        }

        public async Task<Query> Get(Guid queryGuid)
        {
            var result = await _db.Queries.FirstOrDefaultAsync(q => q.QueryId == queryGuid);
            if (result != null)
            {
                result.CountSignIn++;
            }

            await _db.SaveChangesAsync();

            return result;
        }

        public async Task<Guid> Create(Query query)
        {
            await _db.Queries.AddAsync(query);
            await _db.SaveChangesAsync();

            return query.QueryId;
        }

        public async Task Update(Guid queryGuid, Query query)
        {
            var queryToUpdate = await _db.Queries.FirstOrDefaultAsync(q => q.QueryId == queryGuid);
            query = queryToUpdate;
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Query>> GetAll()
        {
            var result = await _db.Queries.ToListAsync();

            return result;
        }
    }
}
