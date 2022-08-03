using GlonassSoft.Infrastructure;
using System;
using System.Threading.Tasks;

namespace GlonassSoft.DAL
{
    public interface IUserStatisticsRepository
    {
        public Task<Query> GetQuery(Guid queryGuid);
        public Task<Guid> CreateQuery(Query query);
        public Task UpdatePercent(Guid queryGuid, byte percent);
        public Task UpdateSigninCount(Guid queryGuid, int signinCount);
    }
}
