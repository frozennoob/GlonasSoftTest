using GlonassSoft.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlonassSoft.DAL
{
    public interface IReportRepository
    {
        public Task<IEnumerable<Query>> GetAll();
        public Task<Query> Get(Guid queryGuid);
        public Task<Guid> Create(Query query);
        public Task Update(Guid queryGuid, Query query);
        public Task Remove(Guid queryGuid);
    }
}
