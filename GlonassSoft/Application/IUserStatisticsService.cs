using GlonassSoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlonassSoft.Application
{
    public interface IUserStatisticsService
    {
        public Task<Guid> RequestReport(Guid userId, DateTime dateFrom, DateTime dateTo);
        public Task<ReportInfo> GetReport(Guid requestGuid);
    }
}
