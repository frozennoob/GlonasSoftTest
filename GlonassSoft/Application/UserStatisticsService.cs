using GlonassSoft.DAL;
using GlonassSoft.Infrastructure;
using GlonassSoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlonassSoft.Application
{
    public class UserStatisticsService : IUserStatisticsService
    {
        private readonly IUserStatisticsRepository _repo;

        public UserStatisticsService(IUserStatisticsRepository repo)
        {
            _repo = repo;
        }

        public Task<ReportInfo> GetReport(Guid requestGuid)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> RequestReport(Guid userId, DateTime dateFrom, DateTime dateTo)
        {
            
        }
    }
}
